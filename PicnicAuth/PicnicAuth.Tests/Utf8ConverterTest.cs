using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class Utf8ConverterTest
    {
        private Utf8Converter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new Utf8Converter();
        }

        [TestCase("abc", ExpectedResult = new byte[] {0x61, 0x62, 0x63})]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = new byte[] {})]
        public byte[] TestConvertToBytes(string input)
        {
            return converter.ConvertToBytes(input);
        }
    }
}
