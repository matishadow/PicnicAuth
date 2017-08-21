using System;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class OtpTruncator : IOtpTruncator
    {
        private const int ValidSha1SignatureLength = 20;

        public string Truncate(byte[] hmacSignature, OtpLength otpLength = OtpLength.SixDigits)
        {
            if (hmacSignature == null) throw new ArgumentNullException();
            if (hmacSignature.Length != ValidSha1SignatureLength)
                throw new ArgumentException(Properties.Resources.InvalidSha1LengthExceptionMessage);

            throw new NotImplementedException();
        }
    }
}