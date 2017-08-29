using System.Drawing;

namespace PicnicAuth.Tests.Image
{
    public interface IBitmapComparer
    {
        bool BitmapsEquals(Bitmap bitmap1, Bitmap bitmap2);
    }
}