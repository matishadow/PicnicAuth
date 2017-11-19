using NUnit.Framework;
using PicnicAuth.Dto;

namespace PicnicAuth.Tests.TestFixtures.Dtos
{
    [TestFixture]
    public class CompanyAccountDtoTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var companyAccountDto = new CompanyAccountDto();

            TestProperty(s => companyAccountDto.UserName = s,
                () => companyAccountDto.UserName);
            TestProperty(s => companyAccountDto.Email = s,
                () => companyAccountDto.Email);
        }
    }
}