using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.Image;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Web;
using QRCoder;
using Swashbuckle.Swagger.Annotations;

namespace PicnicAuth.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class QrCodesController : ApiController
    {
        private readonly IQrCodeGenerator qrCodeGenerator;
        private readonly IKeyUriCreator keyUriCreator;
        private readonly IHttpResponseMessageCreator httpResponseMessageCreator;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDpapiDecryptor dpapiDecryptor;

        public QrCodesController(IQrCodeGenerator qrCodeGenerator,
            IKeyUriCreator keyUriCreator, IHttpResponseMessageCreator httpResponseMessageCreator,
            IUnitOfWork unitOfWork, IDpapiDecryptor dpapiDecryptor)
        {
            this.qrCodeGenerator = qrCodeGenerator;
            this.keyUriCreator = keyUriCreator;
            this.httpResponseMessageCreator = httpResponseMessageCreator;
            this.unitOfWork = unitOfWork;
            this.dpapiDecryptor = dpapiDecryptor;
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
            int pixelPerModule = 20, QRCodeGenerator.ECCLevel level = QRCodeGenerator.ECCLevel.M)
        {
            AuthUser authUser = unitOfWork.Repository<AuthUser>().GetById(authUserId);
            if (authUser == null) return new HttpResponseMessage(HttpStatusCode.NotFound);

            string userSecret = dpapiDecryptor.Decrypt(authUser.Secret);

            string keyUri = keyUriCreator
                .CreateKeyUri(issuer ?? RequestContext.Principal.Identity.GetUserName(), 
                authUser.UserName, userSecret);
            Bitmap bitmap = qrCodeGenerator.GenerateQrCode(keyUri, pixelPerModule, level);
            HttpResponseMessage returnMessage = httpResponseMessageCreator.CreatePngResponse(bitmap);

            return returnMessage;
        }
    }
}
