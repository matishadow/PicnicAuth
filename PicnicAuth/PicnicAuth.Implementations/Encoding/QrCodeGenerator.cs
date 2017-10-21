using System;
using System.Drawing;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Encoding;
using QRCoder;

namespace PicnicAuth.Implementations.Encoding
{
    public class QrCodeGenerator : IQrCodeGenerator, IRequestDependency
    {
        private readonly QRCodeGenerator qrCodeGenerator;

        public QrCodeGenerator()
        {
            qrCodeGenerator = new QRCodeGenerator();
        }

        public Bitmap GenerateQrCode(string text, int pixelPerModule = 20, QRCodeGenerator.ECCLevel errorCorrectionLevel = QRCodeGenerator.ECCLevel.M)
        {
            if (text == null) throw new ArgumentNullException();

            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(text, errorCorrectionLevel);
            var qrCode = new QRCode(qrCodeData);

            return qrCode.GetGraphic(pixelPerModule);
        }

        public void Dispose()
        {
            qrCodeGenerator?.Dispose();
        }
    }
}