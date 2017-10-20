using System;
using System.Collections.Generic;
using System.Linq;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.Interfaces.Time;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class TotpValidator : ITotpValidator, IRequestDependency
    {
        private const int ValidTimeStepSize = 1;
        private const int TotpTimeWindow = 30;

        private readonly IUnixTimestampGetter unixTimestampGetter;
        private readonly IHotpGenerator hotpGenerator;

        public TotpValidator(IUnixTimestampGetter unixTimestampGetter, IHotpGenerator hotpGenerator)
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

            long currentTimestampInput = unixTimestampGetter.GetUnixTimestamp() / TotpTimeWindow;
            IEnumerable<long> consideredTimestampInputs = GetConsideredTimestampInputs(currentTimestampInput);

            IEnumerable<string> totps = consideredTimestampInputs
                .Select(input => hotpGenerator.GenerateHotp(input, secret));

            return totps.Contains(totp);
        }

        private IEnumerable<long> GetConsideredTimestampInputs(long currentTimestampInput)
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