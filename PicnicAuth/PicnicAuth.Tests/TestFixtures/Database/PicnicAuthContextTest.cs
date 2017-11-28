using System.Data.Entity;
using NUnit.Framework;
using PicnicAuth.Database;

namespace PicnicAuth.Tests.TestFixtures.Database
{
    [TestFixture]
    public class PicnicAuthContextTest : PicnicAuthContext
    {
        private PicnicAuthContext picnicAuthContext;

        [SetUp]
        public void CreatePicnicAuthContext()
        {
            picnicAuthContext = new PicnicAuthContext();
        }

        [Test]
        public void TestRegiterEntityConfigurationTypes()
        {
            Assert.DoesNotThrow(() =>
            {
                OnModelCreating(new DbModelBuilder());
            });
        }

        [Test]
        public void TestCreate()
        {
            PicnicAuthContext instance = Create();

            Assert.NotNull(instance);
        }
    }
}