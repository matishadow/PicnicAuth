using System.Net.Http;
using PicnicAuth.Dto;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Models;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Interfaces.Web
{
    public interface IAuthUserDtoFiller
    {
        void FillAuthUserDto(AuthUserDto authUserDto, HttpRequestMessage request, AuthUser authUser,
            CompanyAccount loggedCompany, byte[] secret, IOtpQrCodeUriGenerator otpQrCodeUriGenerator,
            IBase32Encoder base32Encoder);
    }
}