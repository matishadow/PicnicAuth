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
    public class AbstractDatabaseValidatorTest
    {
        [Test]
        public void TestValidate()
        {
            Guid someGuid = Guid.NewGuid();
            var testClass = new TestClass { A = 1, B = 2 };

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .Setup(work => work
                    .Repository<TestClass>().GetById(someGuid))
                .Returns(testClass);

            var abstractDatabaseValidator = new AbstractDatabaseValidator<TestClass>(unitOfWorkMock.Object);

            Assert.DoesNotThrow(() =>
            {
                abstractDatabaseValidator.EntityExists<TestClass>(Guid.NewGuid());
            });
        }

        public class TestClass
        {
            public int A { get; set; }
            public int B { get; set; }
        }
    }
}