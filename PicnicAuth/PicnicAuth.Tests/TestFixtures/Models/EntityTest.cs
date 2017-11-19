using NUnit.Framework;
using PicnicAuth.Models;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class EntityTest : GenericPropertyTest
    {
        [Test]
        public void TestId()
        {
            var entity = new Entity();

            TestProperty(s => entity.Id = s, () => entity.Id);
        }
    }
}