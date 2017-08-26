using System;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class TotpVerifier : ITotpVerifier
    {
        private readonly ITotpGenerator generator;

        public TotpVerifier(ITotpGenerator generator)
        {
            this.generator = generator;
        }

        public bool IsTotpValid(byte[] secret, string totp)
        {
            throw new NotImplementedException();
        }
    }
}