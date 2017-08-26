using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class KeyDerivation : IKeyDerivation
    {
        public byte[] GetDerivedBytes(byte[] passPhrase, byte[] salt, int iterations, int keySizeInBytes)
        {
            Rfc2898DeriveBytes key = null;

            try
            {
                key = new Rfc2898DeriveBytes(passPhrase, salt, iterations);
                byte[] keyBytes = key.GetBytes(keySizeInBytes);
                return keyBytes;
            }
            finally
            {
                key?.Dispose();
            }
        }
    }
}