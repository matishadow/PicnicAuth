using System;
using System.Net.Http;
using PicnicAuth.Enums;

namespace PicnicAuth.Interfaces.Web
{
    public interface IOtpQrCodeUriGenerator
    {
        Uri GenerateQrCodeUri(OtpType otpType, HttpRequestMessage request, Guid userId,
            string companyUsername);
    }
}