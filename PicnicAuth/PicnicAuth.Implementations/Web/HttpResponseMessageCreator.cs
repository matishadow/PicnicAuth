using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Image;
using PicnicAuth.Interfaces.Web;

namespace PicnicAuth.Implementations.Web
{
    public class HttpResponseMessageCreator : IHttpResponseMessageCreator, IRequestDependency
    {
        private const string PngMediaHeader = "image/png";
        private readonly IImageConverter imageConverter;

        public HttpResponseMessageCreator(IImageConverter imageConverter)
        {
            this.imageConverter = imageConverter;
        }

        public HttpResponseMessage CreatePngResponse(Bitmap bitmap)
        {
            System.Drawing.Image pngImage = imageConverter.ConvertBitmapToPng(bitmap);
            byte[] imageBytes = imageConverter.PngImageToBytes(pngImage);

            var returnMessage = new HttpResponseMessage
            {
                Content = new ByteArrayContent(pngImage != null ? imageBytes : new byte[] { })
                {
                    Headers = { ContentType = new MediaTypeHeaderValue(PngMediaHeader) }
                }
            };

            return returnMessage;
        }
    }
}