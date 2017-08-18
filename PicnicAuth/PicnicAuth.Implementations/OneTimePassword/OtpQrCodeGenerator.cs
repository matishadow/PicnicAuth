using System.Drawing;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class OtpQrCodeGenerator : IOtpQrCodeGenerator
    {
        public Bitmap GenerateOtpQrCode(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            throw new System.NotImplementedException();
        }
    }
}
