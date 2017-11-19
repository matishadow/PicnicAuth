using System;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using PicnicAuth.Dto;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.Web;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.Models;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Tests.TestFixtures.Web
{
    [TestFixture]
    public class AuthUserDtoFillerTest
    {
        [Test]
        public void TestFillAuthUserDto()
        {
            var authFiller = new AuthUserDtoFiller();

            const string uriPart = "http://aaaa.com";
            var guid = new Guid("651b14ee-f6e1-4ddb-956d-f46aa3a3fd1f");
            var request = new HttpRequestMessage(HttpMethod.Get, uriPart);
            var authUserDto = new AuthUserDto
            {
                Id = guid
            };
            var authUser = new AuthUser
            {
                Id = guid
            };
            var loggedCompany = new CompanyAccount
            {
                UserName = "aaa@gmail.com"
            };
            byte[] secret = {1, 2, 3};

            var encoderMock = new Mock<IBase32Encoder>();
            encoderMock.Setup(encoder => encoder.Encode(secret))
                .Returns("aaa");

            var generatorMock = new Mock<IOtpQrCodeUriGenerator>();
            generatorMock.Setup(generator =>
                    generator.GenerateQrCodeUri(OtpType.Totp, request, guid, loggedCompany.UserName))
                .Returns(new Uri(uriPart + "/api/qrcodes/" + guid + "?type=totp&issuer=" + loggedCompany.UserName));
            generatorMock.Setup(generator =>
                    generator.GenerateQrCodeUri(OtpType.Hotp, request, guid, loggedCompany.UserName))
                .Returns(new Uri(uriPart + "/api/qrcodes/" + guid + "?type=hotp&issuer=" + loggedCompany.UserName));

            authFiller.FillAuthUserDto(authUserDto, request, authUser, loggedCompany, secret, generatorMock.Object,
                encoderMock.Object);

            Assert.AreEqual(authUserDto.TotpQrCodeUri,
                new Uri("http://aaaa.com/api/qrcodes/651b14ee-f6e1-4ddb-956d-f46aa3a3fd1f?type=totp&issuer=aaa@gmail.com"));
            Assert.AreEqual(authUserDto.HotpQrCodeUri,
                new Uri("http://aaaa.com/api/qrcodes/651b14ee-f6e1-4ddb-956d-f46aa3a3fd1f?type=hotp&issuer=aaa@gmail.com"));
        }
    }
}