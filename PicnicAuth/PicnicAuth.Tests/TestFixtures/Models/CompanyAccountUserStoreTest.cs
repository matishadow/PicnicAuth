using NUnit.Framework;
using PicnicAuth.Database;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class CompanyAccountUserStoreTest
    {
        [Test]
        public void TestConstructor()
        {
            var companyAccountUserStore = new CompanyAccountUserStore(new PicnicAuthContext());

            Assert.NotNull(companyAccountUserStore);
        }
    }
}