using NUnit.Framework;
using PicnicAuth.Models.Authentication.Identity;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class RoleTest
    {
        [Test]
        public void TestEmptyConstructor()
        {
            var role = new Role();

            Assert.NotNull(role);
        }

        [TestCase("aaa")]
        [TestCase("")]
        public void TestConstructor(string name)
        {
            var role = new Role(name);

            Assert.AreEqual(role.Name, name);
        }
    }
}