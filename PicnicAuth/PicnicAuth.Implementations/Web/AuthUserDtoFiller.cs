using System.Net.Http;
using PicnicAuth.Dto;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.Models;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Implementations.Web
{
    public class AuthUserDtoFiller : IAuthUserDtoFiller, IRequestDependency
    {
        public void FillAuthUserDto(AuthUserDto authUserDto, HttpRequestMessage request, AuthUser authUser,
            CompanyAccount loggedCompany, byte[] secret, IOtpQrCodeUriGenerator otpQrCodeUriGenerator,
            IBase32Encoder base32Encoder)
        {
            authUserDto.TotpQrCodeUri = otpQrCodeUriGenerator
                .GenerateQrCodeUri(OtpType.Totp, request, authUser.Id, loggedCompany.UserName);
            authUserDto.HotpQrCodeUri = otpQrCodeUriGenerator
                .GenerateQrCodeUri(OtpType.Hotp, request, authUser.Id, loggedCompany.UserName);
            authUserDto.SecretInBase32 = base32Encoder.Encode(secret);
        }
    }
}