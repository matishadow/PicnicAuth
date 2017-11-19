using NUnit.Framework;
using PicnicAuth.Implementations.Validation;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Validation
{
    [TestFixture]
    public class SetPasswordValidatorTest
    {
        [Test]
        public void TestConstructor()
        {
            var setPasswordValidator = new SetPasswordValidator();

            Assert.IsNotNull(setPasswordValidator);
        }

        [Test]
        public void TestValidation()
        {
            var setPasswordValidator = new SetPasswordValidator();
            var setPasswordBindingModel = new SetPasswordBindingModel()
            {
                ConfirmPassword = "aaaaaaaaaaaaaaaaaaaa",
                NewPassword = "aaaaaaaaaaaaaaaaaaaa"
            };

            setPasswordValidator.Validate(setPasswordBindingModel);
        }
    }
}