using NUnit.Framework;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class TokenResponseTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var tokenResponse = new TokenResponse();

            TestProperty(s => tokenResponse.access_token = s,
                () => tokenResponse.access_token);
            TestProperty(s => tokenResponse.token_type = s,
                () => tokenResponse.token_type);
            TestProperty(s => tokenResponse.expires_in = s,
                () => tokenResponse.expires_in);
            TestProperty(s => tokenResponse.userName = s,
                () => tokenResponse.userName);
            TestProperty(s => tokenResponse.issued = s,
                () => tokenResponse.issued);
            TestProperty(s => tokenResponse.expires = s,
                () => tokenResponse.expires);
        }
    }
}