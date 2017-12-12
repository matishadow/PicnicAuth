using System.Globalization;
using System.Resources;
using NUnit.Framework;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class ResourcesTest 
    {
        [Test]
        public void TestGets()
        {
            ResourceManager value = PicnicAuth.Models.Properties.Resources.ResourceManager;

            Assert.NotNull(value);
        }

        [Test]
        public void TestCulture()
        {
            var culture = new CultureInfo("aaa");

            PicnicAuth.Models.Properties.Resources.Culture = culture;
            Implementations.Properties.Resources.Culture = culture;

            Assert.AreEqual(culture, PicnicAuth.Models.Properties.Resources.Culture);
            Assert.AreEqual(culture, Implementations.Properties.Resources.Culture);
        }

        [Test]
        public void TestConstructor()
        {
            var resources1 = new PicnicAuth.Models.Properties.Resources();
            var resources2 = new Implementations.Properties.Resources();

            Assert.NotNull(resources1);
            Assert.NotNull(resources2);
        }
    }
}