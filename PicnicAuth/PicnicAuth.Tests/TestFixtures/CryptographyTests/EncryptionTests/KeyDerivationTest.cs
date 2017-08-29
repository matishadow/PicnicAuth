using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class KeyDerivationTest
    {
        private static readonly byte[] ExamplePassword = {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef};
        private const int ExampleIterations = 1000;
        private const int ExampleSizeInBytes = 256 / 8;

        private IKeyDerivation keyDerivation;

        [SetUp]
        public void SetUp()
        {
            keyDerivation = new KeyDerivation();
        }

        [TestCase(new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExampleIterations,
            ExpectedResult = new byte[]
            {
                51, 69, 193, 85, 3, 111, 231, 204,
                190, 42, 128, 143, 255, 161, 154, 160,
                168, 112, 97, 172, 170, 9, 166, 237,
                70, 188, 12, 117, 98, 169, 1, 34
            })]
        [TestCase(new byte[] {0x70, 0x61, 0x73, 0x73, 0x77, 0x6f, 0x72, 0x64},
            new byte[] {0x70, 0x61, 0x73, 0x73, 0x77, 0x6f, 0x72, 0x64},
            ExampleIterations,
            ExpectedResult = new byte[]
            {
                0x95, 0x8f, 0x52, 0x68, 0xa3, 0x45, 0xc1, 0x9f,
                0xd7, 0xd7, 0x13, 0xb9, 0x6, 0x6f, 0x4d, 0x6b,
                0x2f, 0xd0, 0x80, 0x79, 0x33, 0xfa, 0x3a, 0xa9,
                0x8a, 0x84, 0x95, 0x7f, 0x61, 0xff, 0x3a, 0xf2
            })]
        public byte[] TestGetDerivedBytes(byte[] password, byte[] salt, int iterations)
        {
            byte[] resultBytes = keyDerivation
                .GetDerivedBytes(password, salt, iterations, ExampleSizeInBytes);

            return resultBytes;
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

        [TestCase(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 0, ExampleIterations)]
        [TestCase(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, -100, ExampleIterations)]
        public void TestGetDerivedBytesInvalidArgumentOutOfRange(byte[] salt, int iterations, int keySizeInBytes)
        {
            Assert.That(
                () => keyDerivation.GetDerivedBytes(ExamplePassword, salt, iterations, keySizeInBytes),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
