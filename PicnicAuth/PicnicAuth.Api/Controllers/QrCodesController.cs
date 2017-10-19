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
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.Image;
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
        private readonly IImageConverter imageConverter;

        public QrCodesController(IQrCodeGenerator qrCodeGenerator, IImageConverter imageConverter)
        {
            this.qrCodeGenerator = qrCodeGenerator;
            this.imageConverter = imageConverter;
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
            Image pngImage = imageConverter.ConvertBitmapToPng(bitmap);
            byte[] imageBytes = imageConverter.PngImageToBytes(pngImage);

            var returnMessage = new HttpResponseMessage
            {
                Content = new ByteArrayContent(pngImage != null ? imageBytes : new byte[] { })
                {
                    Headers = {ContentType = new MediaTypeHeaderValue(Properties.Resources.PngMediaHeader)}
                }
            };

            return returnMessage;
        }
    }
}
