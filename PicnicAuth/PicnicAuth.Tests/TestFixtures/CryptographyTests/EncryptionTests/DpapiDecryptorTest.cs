using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;

namespace PicnicAuth.Tests.TestFixtures.CryptographyTests.EncryptionTests
{
    [TestFixture]
    public class DpapiDecryptorTest
    {
        private readonly DpapiDecryptor dpapiDecryptor = new DpapiDecryptor();
        private readonly DpapiEncryptor dpapiEncryptor = new DpapiEncryptor();

        [Test]
        public void TestDecryptToString()
        {
            byte[] encrypted = dpapiEncryptor.Encrypt("aaa");
            string decrypted = dpapiDecryptor.DecryptToString(encrypted);

            Assert.IsNotNull(decrypted);
        }

        [Test]
        public void TestDecryptToBytes()
        {
            byte[] encrypted = dpapiEncryptor.Encrypt("aaa");
            byte[] decrypted = dpapiDecryptor.DecryptToBytes(encrypted);

            Assert.IsNotNull(decrypted);
        }
    }
}