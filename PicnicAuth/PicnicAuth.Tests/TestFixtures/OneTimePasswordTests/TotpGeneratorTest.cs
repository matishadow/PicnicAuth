using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.Cryptography.Signature;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class TotpGeneratorTest
    {
        private const ulong ExampleTimetamp = 1503511830;
        private static readonly byte[] ExampleSecret = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef };
        private static readonly byte[] ExampleHmac =
        {
            0x90, 0x18, 0x2d, 0x1a, 0xcf, 0xe3, 0x1c, 0x64, 0xc8, 0xb4,
            0x4a, 0xde, 0xde, 0x1a, 0xe7, 0x3a, 0xa9, 0x44, 0x1f, 0xa5
        };

        private ITotpGenerator generator;

        [SetUp]
        public void SetUp()
        {
            var mockUnixTimestampGetter = new Mock<IUnixTimestampGetter>();
            mockUnixTimestampGetter.Setup(getter => getter.GetUnixTimestamp())
                .Returns((long)ExampleTimetamp);

            var mockHmacSha1Generator = new Mock<IHmacSha1Generator>();
            mockHmacSha1Generator.Setup(sha1Generator =>
                    sha1Generator.GenerateHmacSha1Hash(ExampleTimetamp / 30, ExampleSecret))
                .Returns(ExampleHmac);

            var mockOtpTruncator = new Mock<IOtpTruncator>();
            mockOtpTruncator.Setup(truncator =>
                    truncator.Truncate(ExampleHmac, OtpLength.SixDigits))
                .Returns("805192");

            generator = new TotpGenerator(mockUnixTimestampGetter.Object,
                mockHmacSha1Generator.Object, mockOtpTruncator.Object);
        }

        [TestCase(new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = "805192")]
        public string TestGenerateTotp(byte[] secret)
        {
            return generator.GenerateTotp(secret);
        }

        [Test]
        public void TestGenerateTotpNullSecret()
        {
            Assert.That(() => generator.GenerateTotp(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestGenerateTotpEmptySecret()
        {
            Assert.That(() => generator.GenerateTotp(new byte[]{}),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
