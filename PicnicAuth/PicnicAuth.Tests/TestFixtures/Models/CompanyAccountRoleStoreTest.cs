using NUnit.Framework;
using PicnicAuth.Database;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class CompanyAccountRoleStoreTest
    {
        [Test]
        public void TestConstructor()
        {
            var companyAccountRoleStore = new CompanyAccountRoleStore(new PicnicAuthContext());

            Assert.NotNull(companyAccountRoleStore);
        }
    }
}