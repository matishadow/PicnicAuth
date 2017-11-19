using NUnit.Framework;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class ChangePasswordBindingModelTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var changePasswordBindingModel = new ChangePasswordBindingModel();

            TestProperty(s => changePasswordBindingModel.OldPassword = s, 
                () => changePasswordBindingModel.OldPassword);
            TestProperty(s => changePasswordBindingModel.NewPassword = s,
                () => changePasswordBindingModel.NewPassword);
            TestProperty(s => changePasswordBindingModel.ConfirmPassword = s,
                () => changePasswordBindingModel.ConfirmPassword);
        }
    }
}