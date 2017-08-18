using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class LongConverterTest
    {
        private ILongConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new LongConverter();
        }

        [TestCase(10000, ExpectedResult = new byte[] { 0x27, 0x10 })]
        [TestCase(0, ExpectedResult = new byte[] {0})]
        [TestCase(long.MaxValue, 
            ExpectedResult = new byte[] {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff })]
        public byte[] TestConvertToBytes(long input)
        {
            return converter.ConvertToBytes(input);
        }
    }
}
