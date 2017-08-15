using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class SecretGeneratorTest
    {
        private ISecretGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new SecretGenerator();
        }

        [TestCase(ExpectedResult = "JBSWY3DPEHPK3PXP")]
        public string TestGenerateSecret()
        {
            return generator.GenerateSecret();
        }
    }
}
