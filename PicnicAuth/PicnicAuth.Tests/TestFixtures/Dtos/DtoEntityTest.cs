using NUnit.Framework;
using PicnicAuth.Dto;

namespace PicnicAuth.Tests.TestFixtures.Dtos
{
    [TestFixture]
    public class DtoEntityTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var dtoEntity = new DtoEntity();

            TestProperty(s => dtoEntity.Id = s,
                () => dtoEntity.Id);
        }

        [Test]
        public void TestConstructor()
        {
            var dtoEntity = new DtoEntity();

            Assert.IsNotNull(dtoEntity);
        }
    }
}