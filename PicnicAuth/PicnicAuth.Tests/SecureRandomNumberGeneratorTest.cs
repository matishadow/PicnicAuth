using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Randomness;
using PicnicAuth.Interfaces.Cryptography.Randomness;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class SecureRandomNumberGeneratorTest
    {
        private ISecureRandomNumberGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new SecureRandomNumberGenerator();
        }

        [TearDown]
        public void TearDown()
        {
            generator.Dispose();
        }

        [TestCase(1, ExpectedResult = 1)]
        [TestCase(5, ExpectedResult = 5)]
        [TestCase(20, ExpectedResult = 20)]
        public int TestBytesCount(int numberOfBytes)
        {
            return generator.GenerateRandomBytes(numberOfBytes).Length;
        }

        [TestCase(-5)]
        [TestCase(0)]
        [TestCase(sbyte.MaxValue + 1)]
        [TestCase(int.MaxValue)]
        public void TestGenerateRandomBytesArgument(int numberOfBytes)
        {
            Assert.That(() => generator.GenerateRandomBytes(numberOfBytes),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
