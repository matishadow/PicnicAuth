using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class HotpGeneratorTest
    {
        private IHotpGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new HotpGenerator();
        }

        [TestCase(ulong.MinValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = "282760")]
        [TestCase(ulong.MaxValue, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = "939986")]
        [TestCase(1000, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef}, 
            ExpectedResult = "120699")]
        public string TestGenerateHotp(ulong input, byte[] secret)
        {
            return generator.GenerateHotp(input, secret);
        }

        [Test]
        public void TestGenerateHotpNullSecret()
        {
            Assert.That(() => generator.GenerateHotp(It.IsAny<ulong>(), null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestGenerateHotpEmptySecret()
        {
            Assert.That(() => generator.GenerateHotp(It.IsAny<ulong>(), new byte[]{}),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
