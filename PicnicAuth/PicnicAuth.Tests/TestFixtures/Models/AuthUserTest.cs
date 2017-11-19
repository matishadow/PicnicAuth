using NUnit.Framework;
using PicnicAuth.Models;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class AuthUserTest : GenericPropertyTest
    {
        private AuthUser authUser;

        [SetUp]
        public void CreateAuthUser()
        {
            authUser = new AuthUser();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.NotNull(authUser);
        }

        [Test]
        public void TestProperties()
        {
            TestProperty(s => authUser.Secret = s, () => authUser.Secret);
            TestProperty(s => authUser.ExternalId = s, () => authUser.ExternalId);
            TestProperty(s => authUser.UserName = s, () => authUser.UserName);
            TestProperty(s => authUser.Email = s, () => authUser.Email);
            TestProperty(s => authUser.HotpCounter = s, () => authUser.HotpCounter);
            TestProperty(s => authUser.Company = s, () => authUser.Company);
        }
    }
}