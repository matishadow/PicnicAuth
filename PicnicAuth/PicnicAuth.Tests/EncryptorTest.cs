using System;
using System.Security.Cryptography;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class EncryptorTest
    {
        private const int KeySizeInBits = 256;
        private const CipherMode EncryptorCipherMode = CipherMode.CBC;
        private const PaddingMode EncryptorPaddingMode = PaddingMode.PKCS7;
        private const int ExampleIterations = 1000;
        private const int ExampleKeySizeInBytes = 256 / 8;
        private readonly byte[] passwordEncoded = {0x70, 0x61, 0x73, 0x73, 0x77, 0x6f, 0x72, 0x64};
        private static readonly byte[] ExampleIv =
        {
            0x95, 0x8f, 0x52, 0x68, 0xa3, 0x45, 0xc1, 0x9f, 0xd7, 0xd7, 0x13, 0xb9, 0x6, 0x6f, 0x4d, 0x6b, 0x2f,
            0xd0, 0x80, 0x79, 0x33, 0xfa, 0x3a, 0xa9, 0x8a, 0x84, 0x95, 0x7f, 0x61, 0xff, 0x3a, 0xf2
        };

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
            mockKeyDerivation
                .Setup(derivation => derivation.GetDerivedBytes(It.IsAny<byte[]>(), It.IsAny<byte[]>(), ExampleIterations,
                    ExampleKeySizeInBytes)).Returns(new byte[]
                {
                    0x95, 0x8f, 0x52, 0x68, 0xa3, 0x45, 0xc1, 0x9f, 0xd7, 0xd7, 0x13, 0xb9, 0x6, 0x6f, 0x4d, 0x6b, 0x2f,
                    0xd0, 0x80, 0x79, 0x33, 0xfa, 0x3a, 0xa9, 0x8a, 0x84, 0x95, 0x7f, 0x61, 0xff, 0x3a, 0xf2
                });

            var mockCryptoTransformApplier = new Mock<ICryptoTransformApplier>();
            mockCryptoTransformApplier.Setup(applier => applier.ApplyCryptoTransform(It.IsAny<byte[]>(),
                It.IsAny<ICryptoTransform>(), It.IsAny<CryptoStreamMode>())).Returns(new byte[]
            {
                179, 130, 177, 251, 202, 111, 222, 189,
                148, 81, 244, 199, 196, 115, 87, 240,
                165, 171, 179, 222, 55, 26, 70, 178,
                158, 34, 25, 149, 147, 187, 77, 251
            });

            encryptor = new Encryptor(mockRijndaelManagedCreator.Object, mockKeyDerivation.Object,
                mockCryptoTransformApplier.Object);
        }

        [Test]
        public void TestEncrypt()
        {
            byte[] encrypted = encryptor.Encrypt(passwordEncoded, passwordEncoded, ExampleIv, ExampleIv);

            Assert.AreEqual(encrypted, new byte[]
            {
                179, 130, 177, 251, 202, 111, 222, 189,
                148, 81, 244, 199, 196, 115, 87, 240,
                165, 171, 179, 222, 55, 26, 70, 178,
                158, 34, 25, 149, 147, 187, 77, 251
            });
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
