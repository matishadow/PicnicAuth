using NUnit.Framework;

namespace PicnicAuth.Tests.TestFixtures.Database
{
    [TestFixture]
    public class ResourcesTest : GenericPropertyTest
    {
        [Test]
        public void TestConstructor()
        {
            var resources = new PicnicAuth.Database.Properties.Resources();
            Assert.IsNotNull(resources);
        }

        [Test]
        public void TestGets()
        {
            Assert.NotNull(PicnicAuth.Database.Properties.Resources.SwaggerCompanyNotLoggedInMessage);
            Assert.NotNull(PicnicAuth.Database.Properties.Resources.SwaggerAuthUserNotFoundMessage);
            Assert.NotNull(PicnicAuth.Database.Properties.Resources.SwaggerOtpValidationDoneMessage);
            
            Assert.DoesNotThrow(() =>
            {
                TestProperty(s => PicnicAuth.Database.Properties.Resources.Culture = s,
                    () => PicnicAuth.Database.Properties.Resources.Culture);
            });
        }
    }
}