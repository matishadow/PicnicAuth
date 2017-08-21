using PicnicAuth.Enums;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class OtpTruncator : IOtpTruncator
    {
        public string Truncate(byte[] hmacSignature, OtpLength otpLength = OtpLength.SixDigits)
        {
            throw new System.NotImplementedException();
        }
    }
}