using NUnit.Framework;
using PicnicAuth.Database;

namespace PicnicAuth.Tests.TestFixtures.Database
{
    [TestFixture]
    public class PicnicAuthDatabaseInitializerTest : PicnicAuthDatabaseInitializer
    {
        [Test]
        public void TestSeed()
        {
            var picnicAuthContext = new PicnicAuthContextTest();

            Assert.DoesNotThrow(() =>
            {
                Seed(picnicAuthContext);
            });
        }
    }
}