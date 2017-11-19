using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using NUnit.Framework;
using PicnicAuth.Api.Configs;
using PicnicAuth.Database;
using PicnicAuth.Models.Authentication;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class CompanyAccountTest : GenericPropertyTest
    {
        private CompanyAccount companyAccount;

        [SetUp]
        public void CreateCompanyAccount()
        {
            companyAccount = new CompanyAccount();
        }

        [Test]
        public void TestProperties()
        {
            TestProperty(s => companyAccount.AuthUsers = s, 
                () => companyAccount.AuthUsers);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(companyAccount);
        }
    }
}