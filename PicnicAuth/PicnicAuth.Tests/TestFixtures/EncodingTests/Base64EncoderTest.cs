using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests.TestFixtures.EncodingTests
{
    [TestFixture]
    public class Base64EncoderTest
    {
        private IBase64Encoder encoder;

        [SetUp]
        public void SetUp()
        {
            encoder = new Base64Encoder();
        }

        [TestCase("aaaaaa", ExpectedResult = "YWFhYWFh")]
        [TestCase("ąćźż", ExpectedResult = "xIXEh8W6xbw=")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        public string TestEncode(string input)
        {
            return encoder.Encode(input);
        }

        [TestCase(new byte[] {0x61, 0x61, 0x61, 0x61, 0x61, 0x61}, ExpectedResult = "YWFhYWFh")]
        [TestCase(new byte[] {0xc4, 0x85, 0xc4, 0x87, 0xc5, 0xba, 0xc5, 0xbc}, ExpectedResult = "xIXEh8W6xbw=")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase(new byte[]{}, ExpectedResult = "")]
        public string TestEncode(byte[] input)
        {
            return encoder.Encode(input);
        }
    }
}
