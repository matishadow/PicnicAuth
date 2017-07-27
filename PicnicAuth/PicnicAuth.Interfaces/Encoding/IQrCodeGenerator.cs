using System;
using System.Drawing;
using QRCoder;

namespace PicnicAuth.Interfaces.Encoding
{
    public interface IQrCodeGenerator : IDisposable
    {
        Bitmap GenerateQrCode(string text, int size, QRCodeGenerator.ECCLevel errorCorrectionLevel);
    }
}