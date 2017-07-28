using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PicnicAuth.Interfaces.Image;

namespace PicnicAuth.Implementations.Image
{
    public class ImageConverter : IImageConverter
    {
        public System.Drawing.Image ConvertBitmapToPng(Bitmap bitmap)
        {
            if (bitmap == null) throw new ArgumentNullException();

            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                return System.Drawing.Image.FromStream(stream);
            }
        }
    }
}