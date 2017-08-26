using System.Security.Cryptography;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class CryptoTransformApplierTest
    {
        private static readonly byte[] ExamplePassword = {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef};
        private const int ExampleIterations = 1000;
        private const int ExampleSizeInBits = 256;
        private static readonly byte[] ExampleIV = {
            0x95, 0x8f, 0x52, 0x68, 0xa3, 0x45, 0xc1, 0x9f, 0xd7, 0xd7, 0x13, 0xb9, 0x6, 0x6f, 0x4d, 0x6b, 0x2f,
            0xd0, 0x80, 0x79, 0x33, 0xfa, 0x3a, 0xa9, 0x8a, 0x84, 0x95, 0x7f, 0x61, 0xff, 0x3a, 0xf2
        };

        private const int ExampleSizeInBytes = ExampleSizeInBits / 8;

        private ICryptoTransformApplier cryptoTransformApplier;

        [SetUp]
        public void SetUp()
        {
            cryptoTransformApplier = new CryptoTransformApplier();
        }

        [Test]
        public void TestGetDerivedBytes()
        {
            byte[] resultBytes =
                cryptoTransformApplier.ApplyCryptoTransform(ExamplePassword, new RijndaelManaged
                {
                    BlockSize = ExampleSizeInBits,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                }.CreateEncryptor(ExampleIV, ExampleIV), CryptoStreamMode.Write);

            Assert.AreEqual(resultBytes,
                new byte[]
                {
                    179, 130, 177, 251, 202, 111, 222, 189,
                    148, 81, 244, 199, 196, 115, 87, 240,
                    165, 171, 179, 222, 55, 26, 70, 178,
                    158, 34, 25, 149, 147, 187, 77, 251
                });
        }
    }
}
