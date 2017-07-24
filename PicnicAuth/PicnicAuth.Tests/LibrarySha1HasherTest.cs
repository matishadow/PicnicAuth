using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Hashing;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class LibrarySha1HasherTest
    {
        private LibrarySha1Hasher hasher;

        [SetUp]
        public void Init()
        {
            var mockUtf8Converter = new Mock<IUtf8Converter>();
            mockUtf8Converter.Setup(p => p.ConvertToBytes("aaa")).Returns(new byte[] {0x61, 0x61, 0x61});
            mockUtf8Converter.Setup(p => p.ConvertToBytes("ść")).Returns(new byte[] {0xc5, 0x9b, 0xc4, 0x87});

            hasher = new LibrarySha1Hasher(mockUtf8Converter.Object);
        }

        [TestCase("aaa", ExpectedResult = "7E240DE74FB1ED08FA08D38063F6A6A91462A815")]
        [TestCase("ść", ExpectedResult = "B54524B89E0678DA3E4E657591234F29E15C62FC")]
        [TestCase("", ExpectedResult = "DA39A3EE5E6B4B0D3255BFEF95601890AFD80709")]
        [TestCase(null, ExpectedResult = null)]
        public string TestComputeStringHash(string input)
        {
            return hasher.ComputeStringHash(input);
        }
    }
}
