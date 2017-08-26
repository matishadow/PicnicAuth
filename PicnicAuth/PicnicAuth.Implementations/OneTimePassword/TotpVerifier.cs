using System;
using System.Collections.Generic;
using System.Linq;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class TotpVerifier : ITotpVerifier
    {
        private const int ValidTimeStepSize = 1;
        private const int TotpTimeWindow = 30;

        private readonly IUnixTimestampGetter unixTimestampGetter;
        private readonly IHotpGenerator hotpGenerator;

        public TotpVerifier(IUnixTimestampGetter unixTimestampGetter, IHotpGenerator hotpGenerator)
        {
            this.unixTimestampGetter = unixTimestampGetter;
            this.hotpGenerator = hotpGenerator;
        }

        public bool IsTotpValid(byte[] secret, string totp)
        {
            if (secret == null || totp == null)
                throw new ArgumentNullException();
            if (secret.Length == byte.MinValue || string.IsNullOrWhiteSpace(totp))
                throw new ArgumentException();

            ulong currentTimestampInput = (ulong) unixTimestampGetter.GetUnixTimestamp() / TotpTimeWindow;
            IEnumerable<ulong> consideredTimestampInputs = GetConsideredTimestampInputs(currentTimestampInput);

            IEnumerable<string> totps = consideredTimestampInputs
                .Select(input => hotpGenerator.GenerateHotp(input, secret));

            return totps.Contains(totp);
        }

        private IEnumerable<ulong> GetConsideredTimestampInputs(ulong currentTimestampInput)
        {
            return new[]
            {
                currentTimestampInput,
                currentTimestampInput - ValidTimeStepSize,
                currentTimestampInput + ValidTimeStepSize
            };
        }
    }
}