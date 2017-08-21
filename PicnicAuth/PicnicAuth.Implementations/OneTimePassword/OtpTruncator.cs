using System;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class OtpTruncator : IOtpTruncator
    {
        public string Truncate(byte[] hmacSignature, OtpLength otpLength = OtpLength.SixDigits)
        {
            if (hmacSignature == null) throw new ArgumentNullException();

            throw new NotImplementedException();
        }
    }
}