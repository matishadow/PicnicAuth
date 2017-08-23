using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class HotpVerifierTest
    {
        private static readonly byte[] ExampleSecret = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef };

        private const string Hotp1 = "282760";
        private const string Hotp2 = "939986";
        private const string Hotp3 = "120699";
        private const int ExampleCounter = 1000;

        private IHotpVerifier verifier;

        [SetUp]
        public void SetUp()
        {
            var mockHotpGenerator = new Mock<IHotpGenerator>();
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(ulong.MinValue, ExampleSecret))
                .Returns(Hotp1);
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(ulong.MaxValue, ExampleSecret))
                .Returns(Hotp2);
            mockHotpGenerator.Setup(generator => generator.GenerateHotp(1000, ExampleSecret))
                .Returns(Hotp3);

            verifier = new HotpVerifier(mockHotpGenerator.Object);
        }

        [TestCase(ulong.MinValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, Hotp1,
            ExpectedResult = true)]
        [TestCase(ulong.MaxValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, Hotp2,
            ExpectedResult = true)]
        [TestCase(ExampleCounter, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, Hotp3,
            ExpectedResult = true)]
        [TestCase(ulong.MinValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp3,
            ExpectedResult = false)]
        [TestCase(ulong.MaxValue, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp2,
            ExpectedResult = false)]
        [TestCase(ExampleCounter, new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef }, Hotp1,
            ExpectedResult = false)]
        public bool TestIsHotpValid(ulong counter, byte[] secret, string hotp)
        {
            return verifier.IsHotpValid(counter, secret, hotp);
        }

        [TestCase(0, null, "334257")]
        [TestCase(0, new byte[] {0, 0, 0, 0, 0, 0}, null)]
        [TestCase(0, null, null)]
        public void TestGenerateHotpNullArgument(ulong counter, byte[] secret, string hotp)
        {
            Assert.That(() => verifier.IsHotpValid(counter, secret, hotp),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase(0, new byte[] { }, "334257")]
        [TestCase(0, new byte[] {0, 0, 0, 0, 0, 0}, "")]
        [TestCase(0, new byte[] { }, "")]
        public void TestGenerateHotpEmptySecretOrHotp(ulong counter, byte[] secret, string hotp)
        {
            Assert.That(() => verifier.IsHotpValid(counter, secret, hotp),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
