using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.Cryptography.Randomness;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class SecretGeneratorTest
    {
        private const int PreferredSecretLength = 16;
        private ISecretGenerator generator;

        [SetUp]
        public void SetUp()
        {
            var fixedRandomBytes = new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef};
            
            var mockRandomNumberGenerator = new Mock<ISecureRandomNumberGenerator>();
            mockRandomNumberGenerator.Setup(mock => mock.GenerateRandomBytes(10)).Returns(() => fixedRandomBytes);

            var mockBase32Encoder = new Mock<IBase32Encoder>();
            mockBase32Encoder.Setup(mock => mock.Encode(fixedRandomBytes)).Returns("JBSWY3DPEHPK3PXP");

            generator = new SecretGenerator(mockRandomNumberGenerator.Object, mockBase32Encoder.Object);
        }

        [Test]
        public void TestGenerateSecret()
        {
            string secret = generator.GenerateSecret();
            Assert.AreEqual(secret.Length, PreferredSecretLength);
        }
    }
}
