using NUnit.Framework;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Models.Authentication
{
    [TestFixture]
    public class SetPasswordBindingModelTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var setPasswordBindingModel = new SetPasswordBindingModel();

            TestProperty(s => setPasswordBindingModel.NewPassword = s, () => setPasswordBindingModel.NewPassword);
            TestProperty(s => setPasswordBindingModel.ConfirmPassword = s, () => setPasswordBindingModel.ConfirmPassword);
        }
    }
}
