using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class HotpGenerator : IHotpGenerator
    {
        public string GenerateHotp(ulong counter, byte[] secret)
        {
            throw new System.NotImplementedException();
        }
    }
}
