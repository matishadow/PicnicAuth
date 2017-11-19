using NUnit.Framework;
using PicnicAuth.Implementations.Validation;

namespace PicnicAuth.Tests.TestFixtures.Validation
{
    [TestFixture]
    public class AbstractContinueValidatorTest
    {
        [Test]
        public void TestConstructor()
        {
            var abstractContinueValidator = new AbstractContinueValidator<int>();

            Assert.IsNotNull(abstractContinueValidator);
        }
    }
}