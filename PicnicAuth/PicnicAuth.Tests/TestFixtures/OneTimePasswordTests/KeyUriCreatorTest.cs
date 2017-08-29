using System;
using NUnit.Framework;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class KeyUriCreatorTest
    {
        private IKeyUriCreator keyUriCreator;

        [SetUp]
        public void SetUp()
        {
            keyUriCreator = new KeyUriCreator();
        }

        [TestCase("Example", "alice@google.com", "JBSWY3DPEHPK3PXP", OtpType.Totp, ExpectedResult = "otpauth://totp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example")]
        [TestCase("Example", "alice@google.com", "JBSWY3DPEHPK3PXP", OtpType.Hotp, ExpectedResult = "otpauth://hotp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example")]
        public string TestCreateKeyUri(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            return keyUriCreator.CreateKeyUri(issuer, user, secret, otpType);
        }

        [TestCase(null, "alice@google.com", "JBSWY3DPEHPK3PXP", OtpType.Totp)]
        [TestCase("Example", null, "JBSWY3DPEHPK3PXP", OtpType.Totp)]
        [TestCase("Example", "alice@google.com", null, OtpType.Totp)]
        public void TestNullArgumentsCreateKeyUri(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            Assert.That(() => keyUriCreator.CreateKeyUri(issuer, user, secret, otpType),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase("", "alice@google.com", "JBSWY3DPEHPK3PXP", OtpType.Totp)]
        [TestCase("Example", "", "JBSWY3DPEHPK3PXP", OtpType.Totp)]
        [TestCase("Example", "alice@google.com", "", OtpType.Totp)]
        public void TestEmptyArgumentsCreateKeyUri(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            Assert.That(() => keyUriCreator.CreateKeyUri(issuer, user, secret, otpType),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
