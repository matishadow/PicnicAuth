using PicnicAuth.Enums;

namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IOtpTruncator
    {
        string Truncate(byte[] hmacSignature, OtpLength otpLength = OtpLength.SixDigits);
    }
}