using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Web;
using QRCoder;

namespace PicnicAuth.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class QrCodesController : ApiController
    {
        private readonly IQrCodeGenerator qrCodeGenerator;
        private readonly IHttpResponseMessageCreator httpResponseMessageCreator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDpapiDecryptor dpapiDecryptor;
        private readonly IOtpQrCodeGenerator otpQrCodeGenerator;
        private readonly IBase32Encoder base32Encoder;

        public QrCodesController(IQrCodeGenerator qrCodeGenerator,
            IHttpResponseMessageCreator httpResponseMessageCreator,
            IUnitOfWork unitOfWork, IDpapiDecryptor dpapiDecryptor, 
            IOtpQrCodeGenerator otpQrCodeGenerator, IBase32Encoder base32Encoder)
        {
            this.qrCodeGenerator = qrCodeGenerator;
            this.httpResponseMessageCreator = httpResponseMessageCreator;
            this.unitOfWork = unitOfWork;
            this.dpapiDecryptor = dpapiDecryptor;
            this.otpQrCodeGenerator = otpQrCodeGenerator;
            this.base32Encoder = base32Encoder;
        }

        /// <summary>
        /// Generate Qr image for a given string
        /// </summary>
        /// <returns>QR Code in PNG format</returns>
        [Route("api/QrCodes")]
        [HttpGet]
        public HttpResponseMessage CreateQrImage(string input, int pixelPerModule = 20,
            QRCodeGenerator.ECCLevel level = QRCodeGenerator.ECCLevel.M)
        {
            Bitmap bitmap = qrCodeGenerator.GenerateQrCode(input, pixelPerModule, level);
            HttpResponseMessage returnMessage = httpResponseMessageCreator.CreatePngResponse(bitmap);

            return returnMessage;
        }

        /// <summary>
        /// Generate Qr image for a user
        /// </summary>
        /// <returns>QR Code in PNG format</returns>
        [Route("api/QrCodes/{authUserId}")]
        //[Authorize]
        [HttpGet]
        public HttpResponseMessage CreateUserQrImage(Guid authUserId, string issuer = null, 
            int pixelPerModule = 20, QRCodeGenerator.ECCLevel level = QRCodeGenerator.ECCLevel.M,
            OtpType type = OtpType.Totp)
        {
            AuthUser authUser = unitOfWork.Repository<AuthUser>().GetById(authUserId);
            if (authUser == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

            byte[] userSecret = dpapiDecryptor.DecryptToBytes(authUser.Secret);
            string userSecretBase32 = base32Encoder.Encode(userSecret);

            Bitmap otpBitmap =
                otpQrCodeGenerator.
                GenerateOtpQrCode(issuer ?? RequestContext.Principal.Identity.GetUserName(),
                    authUser.UserName, userSecretBase32, type);
            HttpResponseMessage returnMessage = httpResponseMessageCreator.CreatePngResponse(otpBitmap);

            return returnMessage;
        }
    }
}
