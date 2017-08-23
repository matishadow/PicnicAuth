using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class TotpGenerator : ITotpGenerator
    {
        public string GenerateTotp(byte[] secret)
        {
            throw new System.NotImplementedException();
        }
    }
}