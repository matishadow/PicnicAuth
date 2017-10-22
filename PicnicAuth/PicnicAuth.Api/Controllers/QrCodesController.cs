using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PicnicAuth.Database.DAL;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Web;
using PicnicAuth.Models;
using QRCoder;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// Controller responsible for generating Qr Codes.
    /// Capable of setting ECCLevel and PixelPerModule parameters.
    /// </summary>
    public class QrCodesController : ApiController
    {
        private readonly IQrCodeGenerator qrCodeGenerator;
        private readonly IHttpResponseMessageCreator httpResponseMessageCreator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDpapiDecryptor dpapiDecryptor;
        private readonly IOtpQrCodeGenerator otpQrCodeGenerator;
        private readonly IBase32Encoder base32Encoder;

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="qrCodeGenerator"></param>
        /// <param name="httpResponseMessageCreator"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="dpapiDecryptor"></param>
        /// <param name="otpQrCodeGenerator"></param>
        /// <param name="base32Encoder"></param>
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
        /// Generate Qr Code for given string. 
        /// </summary>
        /// <param name="input">Data to encode into QrCode.</param>
        /// <param name="pixelPerModule">The pixel size each b/w module is drawn. (default is 20)</param>
        /// <param name="level">Error correction capability level. (default is M)</param>
        /// <returns>QrCode in PNG format.</returns>
        [SwaggerResponse(HttpStatusCode.OK, Description = "QrCode as ByteArrayContent")]
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
        /// Generate Qr Code for an AuthUser using his Secret in KeyUri format.
        /// </summary>
        /// <param name="authUserId">User's Id in Guid format.</param>
        /// <param name="issuer">Also known as Company name.</param>
        /// <param name="pixelPerModule">The pixel size each b/w module is drawn. (default is 20)</param>
        /// <param name="level">Error correction capability level. (default is M)</param>
        /// <param name="type">Type of OneTimePassword. (default is TOTP)</param>
        /// <returns>QrCode in PNG format as ByteArrayConten.</returns>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "AuthUser has not been found in logged company's collection.")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Description = "Company is not logged in.")]
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
