using System.Security.Cryptography;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class RijndaelManagedCreatorTest
    {
        private IRijndaelManagedCreator creator;

        [SetUp]
        public void SetUp()
        {
            creator = new RijndaelManagedCreator();
        }

        [TestCase(128, CipherMode.CBC, PaddingMode.PKCS7)]
        [TestCase(192, CipherMode.CBC, PaddingMode.ISO10126)]
        [TestCase(256, CipherMode.CBC, PaddingMode.ANSIX923)]
        public void CreateRijndaelManagedTest(int blockSize, CipherMode cipherMode, PaddingMode paddingMode)
        {
            var rijndaelManaged = creator.CreateRijndaelManaged(blockSize, cipherMode, paddingMode);

            Assert.AreEqual(rijndaelManaged.BlockSize, blockSize);
            Assert.AreEqual(rijndaelManaged.Mode, cipherMode);
            Assert.AreEqual(rijndaelManaged.Padding, paddingMode);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(-10)]
        public void CreateRijndaelManagedInvalidBlockSizeTest(int blockSize)
        {
            Assert.That(() => creator.CreateRijndaelManaged(blockSize, It.IsAny<CipherMode>(), It.IsAny<PaddingMode>()),
                Throws.TypeOf<CryptographicException>());
        }
    }
}
