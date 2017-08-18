using System.Drawing;
using PicnicAuth.Enums;

namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IOtpQrCodeGenerator
    {
        Bitmap GenerateOtpQrCode(string issuer, string user, string secret, OtpType otpType = OtpType.Totp);
    }
}