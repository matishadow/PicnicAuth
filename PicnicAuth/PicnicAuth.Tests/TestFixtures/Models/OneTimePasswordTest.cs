using NUnit.Framework;
using PicnicAuth.Models;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class OneTimePasswordTest : GenericPropertyTest
    {
        [Test]
        public void TestProperty()
        {
            var otp = new OneTimePassword();

            TestProperty(s => otp.OtpValue = s, () => otp.OtpValue);
        }
    }
}