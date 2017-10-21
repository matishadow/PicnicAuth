using System;
using System.Net.Http;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.Web
{
    public class OtpQrCodeUriGenerator : IOtpQrCodeUriGenerator, IRequestDependency
    {
        private const string QrCodeUriTemplate = "{0}/api/qrcodes/{1}?type={2}&issuer={3}";

        public Uri GenerateQrCodeUri(OtpType otpType, 
            HttpRequestMessage request, Guid userId, string companyUsername)
        {
            return new Uri(string.Format(QrCodeUriTemplate,
                request.RequestUri.GetLeftPart(UriPartial.Authority),
                userId, otpType.ToString(), companyUsername));
        }
    }
}
