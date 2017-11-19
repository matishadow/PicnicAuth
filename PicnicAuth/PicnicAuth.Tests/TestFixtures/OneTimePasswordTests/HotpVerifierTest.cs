using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class HotpVerifierTest
    {
        private static readonly byte[] ExampleSecret = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef };

        private const string Hotp1 = "282760";
        private const string Hotp2 = "939986";
        private const string Hotp3 = "120699";
        private const long ExampleCounter = 1000;

        private IHotpValidator validator;

        [SetUp]
        public void SetUp()
        {
            var mockHotpGenerator = new Mock<IHotpGenerator>();
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(long.MinValue, ExampleSecret))
                .Returns(Hotp1);
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(long.MaxValue, ExampleSecret))
                .Returns(Hotp2);
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(1000, ExampleSecret))
                .Returns(Hotp3);

            validator = new HotpValidator(mockHotpGenerator.Object);
        }

        [TestCase(long.MinValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, Hotp1,
            ExpectedResult = true)]
        [TestCase(long.MaxValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, Hotp2,
            ExpectedResult = true)]
        [TestCase(ExampleCounter, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, Hotp3,
            ExpectedResult = true)]
        [TestCase(long.MinValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp2,
            ExpectedResult = false)]
        [TestCase(long.MaxValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp3,
            ExpectedResult = false)]
        [TestCase(ExampleCounter, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp1,
            ExpectedResult = false)]
        public bool TestIsHotpValid(long counter, byte[] secret, string hotp)
        {
            return validator.IsHotpValid(counter, secret, hotp);
        }

        [TestCase(long.MinValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp1,
            ExpectedResult = true)]
        [TestCase(long.MaxValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp2,
            ExpectedResult = true)]
        [TestCase(ExampleCounter, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp3,
            ExpectedResult = true)]
        [TestCase(long.MinValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp2,
            ExpectedResult = false)]
        [TestCase(long.MaxValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp3,
            ExpectedResult = false)]
        [TestCase(ExampleCounter, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp1,
            ExpectedResult = false)]
        public bool TestIsHotpValidInWindow(long counter, byte[] secret, string hotp)
        {
            return validator.IsHotpValidInWindow(counter, secret, hotp, l => { });
        }

        [TestCase(long.MinValue, null, "334257")]
        [TestCase(long.MinValue, new byte[] {0, 0, 0, 0, 0, 0}, null)]
        [TestCase(long.MinValue, null, null)]
        public void TestGenerateHotpNullArgument(long counter, byte[] secret, string hotp)
        {
            Assert.That(() => validator.IsHotpValid(counter, secret, hotp),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase(long.MinValue, new byte[] { }, "334257")]
        [TestCase(long.MinValue, new byte[] {0, 0, 0, 0, 0, 0}, "")]
        [TestCase(long.MinValue, new byte[] { }, "")]
        public void TestGenerateHotpEmptySecretOrHotp(long counter, byte[] secret, string hotp)
        {
            Assert.That(() => validator.IsHotpValid(counter, secret, hotp),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
