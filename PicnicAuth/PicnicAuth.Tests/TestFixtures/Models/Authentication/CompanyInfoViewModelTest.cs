using NUnit.Framework;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class CompanyInfoViewModelTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var companyInfoViewModel = new CompanyInfoViewModel();

            TestProperty(s => companyInfoViewModel.Login = s,
                () => companyInfoViewModel.Login);
            TestProperty(s => companyInfoViewModel.HasRegistered = s,
                () => companyInfoViewModel.HasRegistered);
            TestProperty(s => companyInfoViewModel.LoginProvider = s,
                () => companyInfoViewModel.LoginProvider);
        }
    }
}