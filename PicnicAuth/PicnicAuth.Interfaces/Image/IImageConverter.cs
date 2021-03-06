﻿using System.Drawing;

namespace PicnicAuth.Interfaces.Image
{
    public interface IImageConverter
    {
        System.Drawing.Image ConvertBitmapToPng(Bitmap bitmap);
        byte[] PngImageToBytes(System.Drawing.Image image);
    }
}