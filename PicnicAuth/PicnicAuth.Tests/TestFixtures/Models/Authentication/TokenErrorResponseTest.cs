using NUnit.Framework;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class TokenErrorResponseTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var tokenErrorResponse = new TokenErrorResponse();

            TestProperty(s => tokenErrorResponse.error = s, () => tokenErrorResponse.error);
            TestProperty(s => tokenErrorResponse.error_description = s, 
                () => tokenErrorResponse.error_description);
        }
    }
}