using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using NUnit.Framework;
using PicnicAuth.Database.DAL;
using PicnicAuth.Implementations.Validation;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Validation
{
    [TestFixture]
    public class RegisterValidatorTest
    {
        [Test]
        public void TestValidate()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(work =>
                    work.Repository<CompanyAccount>().Get(It.IsAny<Expression<Func<CompanyAccount, bool>>>(), null, ""))
                .Returns(new List<CompanyAccount>());
            var registerBindingModel = new RegisterBindingModel()
            {
                ConfirmPassword = "aaaaaaaaaaaaaaaaaaaa",
                Email = "aaa@gmail.com",
                Password = "aaaaaaaaaaaaaaaaaaaa",
                UserName = "aaa"
            };

            var registerValidator = new RegisterValidator(unitOfWorkMock.Object);

            Assert.DoesNotThrow(
                () =>
                {
                    registerValidator.Validate(registerBindingModel);
                });
        }
    }
}