using System.Drawing;
using System.Net.Http;

namespace PicnicAuth.Interfaces.Web
{
    public interface IHttpResponseMessageCreator
    {
        HttpResponseMessage CreatePngResponse(Bitmap bitmap);
    }
}