using NUnit.Framework;
using PicnicAuth.Dto;

namespace PicnicAuth.Tests.TestFixtures.Dtos
{
    [TestFixture]
    public class AddAuthUserTest : GenericPropertyTest
    {
        [Test]
        public void TestPropeties()
        {
            var addAuthUser = new AddAuthUser();

            TestProperty(s => addAuthUser.ExternalId = s,
                () => addAuthUser.ExternalId);
            TestProperty(s => addAuthUser.UserName = s,
                () => addAuthUser.UserName);
            TestProperty(s => addAuthUser.Email = s,
                () => addAuthUser.Email);
        }
    }
}