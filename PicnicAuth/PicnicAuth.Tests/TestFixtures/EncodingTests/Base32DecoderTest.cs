using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class Base32DecoderTest
    {
        private IBase32Decoder decoder;

        [SetUp]
        public void SetUp()
        {
            decoder = new Base32Decoder();
        }

        [TestCase("MFQWCYLBME======", ExpectedResult = "aaaaaa")]
        [TestCase("YSC4JB6FXLC3Y===", ExpectedResult = "ąćźż")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        public string TestDecode(string encodedData)
        {
            return decoder.Decode(encodedData);
        }
    }
}
