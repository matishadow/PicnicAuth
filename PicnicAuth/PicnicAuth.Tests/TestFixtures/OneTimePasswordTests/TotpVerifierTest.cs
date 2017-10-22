using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class TotpVerifierTest
    {
        private static readonly byte[] ExampleSecret = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef };
        private const long ExampleTimetamp = 1503511830;

        private const string ValidTotp1 = "805192";
        private const string ValidTotp2 = "238563";
        private const string ValidTotp3 = "424405";

        private const string InvalidTotp = "123456";

        private ITotpValidator validator;

        [SetUp]
        public void SetUp()
        {
            const long exampleTimestampInput = ExampleTimetamp / 30;

            var mockHotpGenerator = new Mock<IHotpGenerator>();
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(exampleTimestampInput, ExampleSecret))
                .Returns(ValidTotp1);
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(exampleTimestampInput - 1, ExampleSecret))
                .Returns(ValidTotp2);
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(exampleTimestampInput + 1, ExampleSecret))
                .Returns(ValidTotp3);

            var mockUnixTimestampGetter = new Mock<IUnixTimestampGetter>();
            mockUnixTimestampGetter.Setup(getter => getter.GetUnixTimestamp())
                .Returns((long)ExampleTimetamp);

            validator = new TotpValidator(mockUnixTimestampGetter.Object, mockHotpGenerator.Object);
        }

        [TestCase(new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, ValidTotp1,
            ExpectedResult = true)]
        [TestCase(new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, ValidTotp2,
            ExpectedResult = true)]
        [TestCase(new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, ValidTotp3,
            ExpectedResult = true)]
        [TestCase(new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, InvalidTotp,
            ExpectedResult = false)]
        public bool TestIsTotpValid(byte[] secret, string hotp)
        {
            return validator.IsTotpValid(secret, hotp);
        }

        [TestCase(null, "334257")]
        [TestCase(new byte[] {0, 0, 0, 0, 0, 0}, null)]
        [TestCase(null, null)]
        public void TestGenerateHotpNullArgument(byte[] secret, string hotp)
        {
            Assert.That(() => validator.IsTotpValid(secret, hotp),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase(new byte[] { }, "334257")]
        [TestCase(new byte[] {0, 0, 0, 0, 0, 0}, "")]
        [TestCase(new byte[] { }, "")]
        public void TestGenerateHotpEmptySecretOrHotp(byte[] secret, string hotp)
        {
            Assert.That(() => validator.IsTotpValid(secret, hotp),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
