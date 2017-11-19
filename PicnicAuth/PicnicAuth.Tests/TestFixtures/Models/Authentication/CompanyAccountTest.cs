using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Moq;
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

        [Test]
        public void TestGenerateUserIdentityAsync()
        {
            var userStoreMock = new Mock<IUserStore<CompanyAccount, Guid>>();

            var userManagerMock = new Mock<UserManager<CompanyAccount, Guid>>(MockBehavior.Default, userStoreMock.Object);
            userManagerMock.Setup(manager => manager.CreateIdentityAsync(It.IsAny<CompanyAccount>(), "Bearer"))
                .Returns(Task.Run(() => new ClaimsIdentity()));

            ClaimsIdentity identity = companyAccount.
                GenerateUserIdentityAsync(userManagerMock.Object, "Bearer").Result;

            Assert.IsNotNull(identity);
        }
    }
}