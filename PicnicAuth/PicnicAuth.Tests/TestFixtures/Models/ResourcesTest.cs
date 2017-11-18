using System.Globalization;
using NUnit.Framework;

namespace PicnicAuth.Tests.TestFixtures.Models
{
    [TestFixture]
    public class ResourcesTest
    {
        [Test]
        public void TestGets()
        {
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.ConfirmPasswordEmptyValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.ConfirmPasswordValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.CurrentPasswordEmptyValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.CurrentPasswordValidationError);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.EmailEmptyValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.EmailExistsValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.EmailRegexValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.EmptyGenericValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.GenericNotOwnerDeleteMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.GenericNowOwnerPutMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.LoginEmptyValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.NewPasswordEmptyValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.PasswordLengthValidationMessage);
            Assert.IsInstanceOf<string>(PicnicAuth.Models.Properties.Resources.UsernameExistsValidationMessage);
        }

        [Test]
        public void TestCulture()
        {
            var culture = new CultureInfo("aaa");

            PicnicAuth.Models.Properties.Resources.Culture = culture;

            Assert.AreEqual(culture, PicnicAuth.Models.Properties.Resources.Culture);
        }

        [Test]
        public void TestConstructor()
        {
            var resources = new PicnicAuth.Models.Properties.Resources();

            Assert.NotNull(resources);
        }
    }
}