using System;
using PicnicAuth.Interfaces.Cryptography.Signature;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class TotpGenerator : ITotpGenerator
    {
        private const int TotpTimeWindow = 30;

        private readonly IUnixTimestampGetter unixTimestampGetter;
        private readonly IHmacSha1Generator hmacSha1Generator;
        private readonly IOtpTruncator otpTruncator;

        public TotpGenerator(IUnixTimestampGetter unixTimestampGetter, IHmacSha1Generator hmacSha1Generator,
            IOtpTruncator otpTruncator)
        {
            this.unixTimestampGetter = unixTimestampGetter;
            this.hmacSha1Generator = hmacSha1Generator;
            this.otpTruncator = otpTruncator;
        }

        public string GenerateTotp(byte[] secret)
        {
            if (secret == null)
                throw new ArgumentNullException();
            if (secret.Length == byte.MinValue)
                throw new ArgumentException();

            var currentTimestamp = (ulong)unixTimestampGetter.GetUnixTimestamp();
            byte[] hmac = hmacSha1Generator.GenerateHmacSha1Hash(currentTimestamp / TotpTimeWindow, secret);
            string otp = otpTruncator.Truncate(hmac);

            return otp;
        }
    }
}