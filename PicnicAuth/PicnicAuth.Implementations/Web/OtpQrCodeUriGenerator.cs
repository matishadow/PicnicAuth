﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
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
