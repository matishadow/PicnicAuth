using System;
using System.Drawing;
using Moq;
using NUnit.Framework;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Tests.Image;
using QRCoder;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class OtpQrCodeGenerator
    {
        private const string ExampleIssuer = "Example";
        private const string ExampleUser = "alice@google.com";
        private const string ExampleSecret = "JBSWY3DPEHPK3PXP";
        private const OtpType ExampleOtpType = OtpType.Totp;
        private const string KeyUriResult =
            "otpauth://totp/Example:alice@google.com?secret=JBSWY3DPEHPK3PXP&issuer=Example";

        private IOtpQrCodeGenerator generator;
        private IBitmapComparer bitmapComparer;

        [SetUp]
        public void SetUp()
        {
            var mockKeyUriCreator = new Mock<IKeyUriCreator>();
            mockKeyUriCreator
                .Setup(p => p.CreateKeyUri(ExampleIssuer, ExampleUser, ExampleSecret, ExampleOtpType))
                .Returns(KeyUriResult);
            var mockQrCodeGenerator = new Mock<IQrCodeGenerator>();
            mockQrCodeGenerator
                .Setup(m => m.GenerateQrCode(KeyUriResult, 20, QRCodeGenerator.ECCLevel.M))
                .Returns(Properties.Resources.ExampleOtpQrCode);

            generator = new Implementations.OneTimePassword.
                OtpQrCodeGenerator(mockQrCodeGenerator.Object, mockKeyUriCreator.Object);
            bitmapComparer = new BitmapComparer();
        }

        [TearDown]
        public void TearDown()
        {
            generator?.Dispose();
        }
    }
}
