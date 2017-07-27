using System.Drawing;
using PicnicAuth.Interfaces.Encoding;
using QRCoder;

namespace PicnicAuth.Implementations.Encoding
{
    public class QrCodeGenerator : IQrCodeGenerator
    {
        private readonly QRCodeGenerator qrCodeGenerator;

        public QrCodeGenerator()
        {
            qrCodeGenerator = new QRCodeGenerator();
        }

        public Bitmap GenerateQrCode(string text, int size, QRCodeGenerator.ECCLevel errorCorrectionLevel)
        {
            QRCodeData qrCodeData = qrCodeGenerator.CreateQrCode(text, errorCorrectionLevel);
            var qrCode = new QRCode(qrCodeData);

            return qrCode.GetGraphic(size);
        }

        public void Dispose()
        {
            qrCodeGenerator?.Dispose();
        }
    }
}