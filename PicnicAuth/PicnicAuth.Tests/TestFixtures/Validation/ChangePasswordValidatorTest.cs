using NUnit.Framework;
using PicnicAuth.Implementations.Validation;

namespace PicnicAuth.Tests.TestFixtures.Validation
{
    [TestFixture]
    public class ChangePasswordValidatorTest
    {
        [Test]
        public void TestConstructor()
        {
            var changePasswordValidator = new ChangePasswordValidator();

            Assert.IsNotNull(changePasswordValidator);
        }
    }
}