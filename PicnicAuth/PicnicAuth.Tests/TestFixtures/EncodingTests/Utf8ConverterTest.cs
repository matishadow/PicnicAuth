using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests.TestFixtures.EncodingTests
{
    [TestFixture]
    public class Utf8ConverterTest
    {
        private IUtf8Converter converter;

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
