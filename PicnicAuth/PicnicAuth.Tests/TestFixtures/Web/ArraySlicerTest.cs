using System;
using System.Net.Http;
using NUnit.Framework;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.Collections;
using PicnicAuth.Implementations.Web;
using PicnicAuth.Interfaces.Collections;
using PicnicAuth.Interfaces.Web;

namespace PicnicAuth.Tests.TestFixtures.Web
{
    [TestFixture]
    public class OtpQrCodeUriGeneratorTest
    {
        private IOtpQrCodeUriGenerator uriGenerator;

        [SetUp]
        public void SetUp()
        {
            uriGenerator = new OtpQrCodeUriGenerator();
        }

        [Test]
        public void GenerateUri()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "http://someurl.com/awdaw/awd");
            var userGuid = new Guid("4c32af93-3220-4b52-be8b-e9b6e1bdc852");
            const string companyUserName = "aaabbbccc";


            Uri resultUri = uriGenerator.GenerateQrCodeUri(OtpType.Totp, message, userGuid, companyUserName);

            Assert.AreEqual(resultUri, 
                new Uri("http://someurl.com/api/qrcodes/4c32af93-3220-4b52-be8b-e9b6e1bdc852?type=Totp&issuer=aaabbbccc"));
        }
    }
}
