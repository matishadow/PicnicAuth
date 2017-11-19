using NUnit.Framework;
using PicnicAuth.Database;

namespace PicnicAuth.Tests.TestFixtures.Database
{
    [TestFixture]
    public class TestPicnicAuthContext
    {
        [Test]
        public void TestCreate()
        {
            PicnicAuthContext db = PicnicAuthContext.Create();

            Assert.IsNotNull(db);
        }
    }
}