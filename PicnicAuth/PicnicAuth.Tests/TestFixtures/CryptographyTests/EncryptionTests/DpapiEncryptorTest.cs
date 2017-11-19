using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Encryption;

namespace PicnicAuth.Tests.TestFixtures.CryptographyTests.EncryptionTests
{
    [TestFixture]
    public class DpapiEncryptorTest
    {
        private readonly DpapiEncryptor dpapiEncryptor = new DpapiEncryptor();

        [Test]
        public void TestEncryptBytes()
        {
            var bytes = new byte[] {1, 2, 3, 4, 5};

            byte[] encrypted = dpapiEncryptor.Encrypt(bytes, bytes);

            Assert.NotNull(encrypted);
        }

        [Test]
        public void TestEncryptString()
        {
            const string someString = "aaa";

            byte[] encrypted = dpapiEncryptor.Encrypt(someString);

            Assert.NotNull(encrypted);
        }
    }
}