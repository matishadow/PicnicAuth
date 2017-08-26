using System;
using System.Security.Cryptography;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class EncrypterTest
    {
        private const int KeySizeInBits = 256;
        private const CipherMode EncryptorCipherMode = CipherMode.CBC;
        private const PaddingMode EncryptorPaddingMode = PaddingMode.PKCS7;

        private IEncryptor encryptor;

        [SetUp]
        public void SetUp()
        {
            var mockRijndaelManagedCreator = new Mock<IRijndaelManagedCreator>();
            mockRijndaelManagedCreator
                .Setup(creator => creator.CreateRijndaelManaged(KeySizeInBits, EncryptorCipherMode,
                    EncryptorPaddingMode)).Returns(
                    new RijndaelManaged
                    {
                        BlockSize = KeySizeInBits,
                        Mode = EncryptorCipherMode,
                        Padding = EncryptorPaddingMode
                    });

            var mockKeyDerivation = new Mock<IKeyDerivation>();
            var mockCryptoTransformApplier = new Mock<ICryptoTransformApplier>();

            encryptor = new Encryptor(mockRijndaelManagedCreator.Object, mockKeyDerivation.Object,
                mockCryptoTransformApplier.Object);
        }

        public byte[] TestEncrypt(byte[] bytesToEncrypt, byte[] passPhrase, byte[] salt, byte[] iv)
        {
            return encryptor.Encrypt(bytesToEncrypt, passPhrase, salt, iv);
        }

        [TestCase(null, new byte[] { }, new byte[] { }, new byte[] { })]
        [TestCase(new byte[] { }, null, new byte[] { }, new byte[] { })]
        [TestCase(new byte[] { }, new byte[] { }, null, new byte[] { })]
        [TestCase(new byte[] { }, new byte[] { }, new byte[] { }, null)] 
        public void TestEncryptNullArgument(byte[] bytesToEncrypt, byte[] passPhrase, byte[] salt, byte[] iv)
        {
            Assert.That(() => encryptor.Encrypt(bytesToEncrypt, passPhrase, salt, iv),
                Throws.TypeOf<ArgumentNullException>());
        }
    }
}
