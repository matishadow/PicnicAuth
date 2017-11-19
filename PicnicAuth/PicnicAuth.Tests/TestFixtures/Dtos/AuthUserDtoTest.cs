using NUnit.Framework;
using PicnicAuth.Dto;

namespace PicnicAuth.Tests.TestFixtures.Dtos
{
    [TestFixture]
    public class AuthUserDtoTest : GenericPropertyTest
    {
        [Test]
        public void TestProperties()
        {
            var authUserDto = new AuthUserDto();

            TestProperty(s => authUserDto.ExternalId = s,
                () => authUserDto.ExternalId);
            TestProperty(s => authUserDto.UserName = s,
                () => authUserDto.UserName);
            TestProperty(s => authUserDto.Email = s,
                () => authUserDto.Email);
            TestProperty(s => authUserDto.HotpQrCodeUri = s,
                () => authUserDto.HotpQrCodeUri);
            TestProperty(s => authUserDto.TotpQrCodeUri = s,
                () => authUserDto.TotpQrCodeUri);
            TestProperty(s => authUserDto.SecretInBase32 = s,
                () => authUserDto.SecretInBase32);
        }
    }
}