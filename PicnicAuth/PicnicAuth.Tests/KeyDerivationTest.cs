using System;
using System.Security.Cryptography;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class KeyDerivationTest
    {
        private static readonly byte[] ExamplePassword = { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef };
        private const int ExampleIterations = 1000;
        private const int ExampleSizeInBytes = 256 / 8;

        private IKeyDerivation keyDerivation;

        [SetUp]
        public void SetUp()
        {
            keyDerivation = new KeyDerivation();
        }

        [Test]
        public void TestGetDerivedBytes()
        {
            byte[] resultBytes = keyDerivation
                .GetDerivedBytes(ExamplePassword, ExamplePassword, ExampleIterations, ExampleSizeInBytes);

            Assert.AreEqual(resultBytes,
                new byte[]
                {
                    51, 69, 193, 85, 3, 111, 231, 204,
                    190, 42, 128, 143, 255, 161, 154, 160,
                    168, 112, 97, 172, 170, 9, 166, 237,
                    70, 188, 12, 117, 98, 169, 1, 34
                });
        }

        [Test]
        public void TestGetDerivedBytesNullSalt()
        {
            Assert.That(
                () => keyDerivation.GetDerivedBytes(ExamplePassword, null, ExampleIterations, ExampleSizeInBytes),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase(new byte[] {1, 2, 3, 4, 5, 6}, ExampleIterations, ExampleIterations)]
        [TestCase(new byte[] {1, 2, 3}, ExampleIterations, ExampleIterations)]
        [TestCase(new byte[] { }, ExampleIterations, ExampleIterations)]
        public void TestGetDerivedBytesInvalidArgument(byte[] salt, int iterations, int keySizeInBytes)
        {
            Assert.That(
                () => keyDerivation.GetDerivedBytes(ExamplePassword, salt, iterations, keySizeInBytes),
                Throws.TypeOf<ArgumentException>());
        }

        [TestCase(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, 0, ExampleIterations)]
        [TestCase(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, -100, ExampleIterations)]
        public void TestGetDerivedBytesInvalidArgumentOutOfRange(byte[] salt, int iterations, int keySizeInBytes)
        {
            Assert.That(
                () => keyDerivation.GetDerivedBytes(ExamplePassword, salt, iterations, keySizeInBytes),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
