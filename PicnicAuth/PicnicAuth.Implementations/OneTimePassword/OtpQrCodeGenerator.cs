using System.Drawing;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class OtpQrCodeGenerator : IOtpQrCodeGenerator
    {
        private readonly IQrCodeGenerator qrCodeGenerator;
        private readonly IKeyUriCreator keyUriCreator;

        public OtpQrCodeGenerator(IQrCodeGenerator qrCodeGenerator, IKeyUriCreator keyUriCreator)
        {
            this.qrCodeGenerator = qrCodeGenerator;
            this.keyUriCreator = keyUriCreator;
        }

        public Bitmap GenerateOtpQrCode(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            string keyUri = keyUriCreator.CreateKeyUri(issuer, user, secret, otpType);

            Bitmap qrCode = qrCodeGenerator.GenerateQrCode(keyUri);

            return qrCode;
        }

        public void Dispose()
        {
            qrCodeGenerator?.Dispose();
        }
    }
}
