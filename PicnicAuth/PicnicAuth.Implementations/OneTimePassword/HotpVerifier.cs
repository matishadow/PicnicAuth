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

        public bool IsHotpValid(ulong counter, byte[] secret, string hotp)
        {
            throw new System.NotImplementedException();
        }
    }
}