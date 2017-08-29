using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests.TestFixtures.EncodingTests
{
    [TestFixture]
    public class UIntConverterTest
    {
        private IUIntConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new UIntConverter();
        }

        [Test]
        public void TestConvertToIntBigEndianNullArgument()
        {
            Assert.That(() => converter.ConvertToIntBigEndian(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestConvertToIntBigEndianTooBigArray()
        {
            Assert.That(() => converter.ConvertToIntBigEndian(new byte[]{0, 0, 0, 0, 0, 0}),
                Throws.TypeOf<ArgumentException>());
        }

        [TestCase(new byte[] {0, 0, 0, 0}, ExpectedResult = 0)]
        [TestCase(new byte[] {0, 0, 0, 2}, ExpectedResult = 2)]
        [TestCase(new byte[] {1}, ExpectedResult = 1)]
        [TestCase(new byte[] {1, 2}, ExpectedResult = 258)]
        [TestCase(new byte[] {1, 2, 3}, ExpectedResult = 66051)]
        [TestCase(new byte[] {1, 2, 3, 4}, ExpectedResult = 16909060)]
        public uint TestConvertToIntBigEndian(byte[] bytes)
        {
            return converter.ConvertToIntBigEndian(bytes);
        }
    }
}
