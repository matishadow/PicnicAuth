using System.Drawing;

namespace PicnicAuth.Tests.Image
{
    public class BitmapComparer : IBitmapComparer
    {
        public bool BitmapsEquals(Bitmap bitmap1, Bitmap bitmap2)
        {
            if (!bitmap1.Size.Equals(bitmap2.Size))
                return false;

            for (int x = 0; x < bitmap1.Width; ++x)
            for (int y = 0; y < bitmap1.Height; ++y)
                if (bitmap1.GetPixel(x, y) != bitmap2.GetPixel(x, y))
                    return false;

            return true;
        }
    }
}