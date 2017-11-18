using NUnit.Framework;
using PicnicAuth.Models;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class OtpValidationResultTest : GenericPropertyTest
    {
        [Test]
        public void TestIsOtpValid()
        {
            var otpValidationResult = new OtpValidationResult();

            TestProperty(s => otpValidationResult.IsOtpValid = s, () => otpValidationResult.IsOtpValid);
        }
    }
}