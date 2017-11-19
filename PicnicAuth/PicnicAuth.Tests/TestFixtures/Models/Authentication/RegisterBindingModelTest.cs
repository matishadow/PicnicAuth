using NUnit.Framework;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class RegisterBindingModelTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var registerBindingModel = new RegisterBindingModel();

            TestProperty(s => registerBindingModel.Email = s,
                () => registerBindingModel.Email);
            TestProperty(s => registerBindingModel.UserName = s,
                () => registerBindingModel.UserName);
            TestProperty(s => registerBindingModel.Password = s,
                () => registerBindingModel.Password);
            TestProperty(s => registerBindingModel.ConfirmPassword = s,
                () => registerBindingModel.ConfirmPassword);
        }
    }
}