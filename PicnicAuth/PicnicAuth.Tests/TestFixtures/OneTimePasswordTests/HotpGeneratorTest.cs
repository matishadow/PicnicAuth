using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.Cryptography.Signature;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class HotpGeneratorTest
    {
        private static readonly byte[] ExampleSecret = {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef};

        private static readonly byte[] hmac1 =
        {
            0x2c, 0xd8, 0xe9, 0x4, 0xe3, 0x95, 0xa7, 0x41, 0x48, 0x77,
            0xaa, 0x2f, 0x82, 0x61, 0xe1, 0x24, 0x35, 0x72, 0xc4, 0x35
        };
        private static readonly byte[] hmac2 =
        {
            0x4a, 0xc3, 0xda, 0x7b, 0x65, 0x71, 0x8b, 0x12, 0xe5, 0x43,
            0x6e, 0x1d, 0xf9, 0x63, 0x4a, 0x51, 0x8a, 0x39, 0x87, 0xa4
        };
        private static readonly byte[] hmac3 =
        {
            0xa7, 0xfd, 0x5b, 0x7, 0xf, 0x6f, 0x3a, 0xb, 0xa8, 0xd6,
            0x1c, 0xbb, 0x4f, 0xd5, 0xe0, 0x95, 0x19, 0x18, 0x8c, 0x8
        };

        private const long ExampleCounter = 1000;

        private IHotpGenerator generator;

        [SetUp]
        public void SetUp()
        {
            var mockHmacSha1Generator = new Mock<IHmacSha1Generator>();
            mockHmacSha1Generator
                .Setup(sha1Generator => sha1Generator.GenerateHmacSha1Hash(long.MinValue, ExampleSecret))
                .Returns(hmac1);
            mockHmacSha1Generator
                .Setup(sha1Generator => sha1Generator.GenerateHmacSha1Hash(long.MaxValue, ExampleSecret))
                .Returns(hmac2);
            mockHmacSha1Generator
                .Setup(sha1Generator => sha1Generator.GenerateHmacSha1Hash(ExampleCounter, ExampleSecret))
                .Returns(hmac3);

            var mockOtpTruncator = new Mock<IOtpTruncator>();
            mockOtpTruncator
                .Setup(truncator => truncator.Truncate(hmac1, OtpLength.SixDigits))
                .Returns("282760");
            mockOtpTruncator
                .Setup(truncator => truncator.Truncate(hmac2, OtpLength.SixDigits))
                .Returns("939986");
            mockOtpTruncator
                .Setup(truncator => truncator.Truncate(hmac3, OtpLength.SixDigits))
                .Returns("120699");

            generator = new HotpGenerator(mockHmacSha1Generator.Object, mockOtpTruncator.Object);
        }

        [TestCase(long.MinValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = "282760")]
        [TestCase(long.MaxValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = "939986")]
        [TestCase(ExampleCounter, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, 
            ExpectedResult = "120699")]
        public string TestGenerateHotp(long input, byte[] secret)
        {
            return generator.GenerateHotp(input, secret);
        }

        [Test]
        public void TestGenerateHotpNullSecret()
        {
            Assert.That(() => generator.GenerateHotp(It.IsAny<long>(), null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestGenerateHotpEmptySecret()
        {
            Assert.That(() => generator.GenerateHotp(It.IsAny<long>(), new byte[]{}),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
