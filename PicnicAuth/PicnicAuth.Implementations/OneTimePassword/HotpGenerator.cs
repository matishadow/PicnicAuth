using System;
using PicnicAuth.Interfaces.Cryptography.Signature;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class HotpGenerator : IHotpGenerator
    {
        private readonly IHmacSha1Generator hmacSha1Generator;
        private readonly IOtpTruncator otpTruncator;

        public HotpGenerator(IHmacSha1Generator hmacSha1Generator, IOtpTruncator otpTruncator)
        {
            this.hmacSha1Generator = hmacSha1Generator;
            this.otpTruncator = otpTruncator;
        }

        public string GenerateHotp(ulong counter, byte[] secret)
        {
            if (secret == null)
                throw new ArgumentNullException();
            if (secret.Length == byte.MinValue)
                throw new ArgumentException();

            byte[] hmac = hmacSha1Generator.GenerateHmacSha1Hash(counter, secret);
            string otp = otpTruncator.Truncate(hmac);
            return otp;
        }
    }
}
