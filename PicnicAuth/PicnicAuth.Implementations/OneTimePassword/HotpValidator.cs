using System;
using System.Collections.Generic;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class HotpValidator : IHotpValidator, IRequestDependency
    {
        private const int ValidHotpWindow = 5;

        private readonly IHotpGenerator generator;

        public HotpValidator(IHotpGenerator generator)
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

        public bool IsHotpValidInWindow(long counter, byte[] secret, string givenHotp, Action<long> setCounter)
        {
            for (var i = 0; i <= ValidHotpWindow; i++)
            {
                string possibleHotp = generator.GenerateHotp(counter + i, secret);
                if (possibleHotp != givenHotp) continue;

                setCounter(counter + i + 1);
                return true;
            }

            return false;
        }
    }
}