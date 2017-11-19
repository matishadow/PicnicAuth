using NUnit.Framework;
using PicnicAuth.Dto;

namespace PicnicAuth.Tests.TestFixtures.Dtos
{
    [TestFixture]
    public class AuthUserInCompanyTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var authUserInCompany = new AuthUserInCompany();

            TestProperty(s => authUserInCompany.ExternalId = s,
                () => authUserInCompany.ExternalId);
            TestProperty(s => authUserInCompany.UserName = s,
                () => authUserInCompany.UserName);
            TestProperty(s => authUserInCompany.Email = s,
                () => authUserInCompany.Email);
        }
    }
}