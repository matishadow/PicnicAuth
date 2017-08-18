using System;
using System.Drawing;
using QRCoder;

namespace PicnicAuth.Interfaces.Encoding
{
    public interface IQrCodeGenerator : IDisposable
    {
        Bitmap GenerateQrCode(string text, int pixelPerModule = 20, QRCodeGenerator.ECCLevel errorCorrectionLevel = QRCodeGenerator.ECCLevel.M);
    }
}