using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http.Controllers;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PicnicAuth.Database.DAL;
using PicnicAuth.Implementations.Web;
using PicnicAuth.Models.Authentication;
using PicnicAuth.Tests.TestFixtures.Validation;

namespace PicnicAuth.Tests.TestFixtures.Web
{
    [TestFixture]
    public class LoggedCompanyGetterTest
    {
        private LoggedCompanyGetter loggedCompanyGetter;
        private Guid someGuid = new Guid();

        [SetUp]
        public void CreateLoggedCompanyGetter()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var genericRepositoryMock = new Mock<IGenericRepository<CompanyAccount>>();
            genericRepositoryMock.Setup(repository => repository.GetById(someGuid))
                .Returns(new CompanyAccount());
            unitOfWorkMock
                .Setup(work =>
                    work.Repository<CompanyAccount>())
                .Returns(genericRepositoryMock.Object);

            loggedCompanyGetter = new LoggedCompanyGetter(unitOfWorkMock.Object);
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(loggedCompanyGetter);
        }

        [Test]
        public void TestGetLoggedCompany()
        {
            var claim = new Claim(someGuid.ToString(), someGuid.ToString()); 
            var mockIdentity =
                Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);

            var principalMock = Mock.Of<IPrincipal>(ip => ip.Identity == mockIdentity);

            var httpRequestContext =
                new HttpRequestContext {Principal = principalMock};

            Assert.DoesNotThrow(() =>
            {
                loggedCompanyGetter.GetLoggedCompany(httpRequestContext);
            });
        }
    }
}