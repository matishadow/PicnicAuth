using System;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class HotpVerifier : IHotpVerifier
    {
        private readonly IHotpGenerator generator;

        public HotpVerifier(IHotpGenerator generator)
        {
            this.generator = generator;
        }

        public bool IsHotpValid(long counter, byte[] secret, string hotp)
        {
            if (secret == null || hotp == null)
                throw new ArgumentNullException();
            if (secret.Length == byte.MinValue || string.IsNullOrWhiteSpace(hotp))
                throw new ArgumentException();

            return generator.GenerateHotp(counter, secret) == hotp;
        }
    }
}