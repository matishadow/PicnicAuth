using Castle.DynamicProxy.Generators.Emitters;
using NUnit.Framework;
using PicnicAuth.Dto;

namespace PicnicAuth.Tests.TestFixtures.Dtos
{
    [TestFixture]
    public class AuthUsersInCompanyTest
    {
        [Test]
        public void TestProperties()
        {
            var authUsersInCompany = new AuthUsersInCompany();

            var authUsers = new AuthUserInCompany[0];
            authUsersInCompany.AuthUsers = authUsers;
            
            Assert.AreSame(authUsersInCompany.AuthUsers, authUsers);
        }
    }
}