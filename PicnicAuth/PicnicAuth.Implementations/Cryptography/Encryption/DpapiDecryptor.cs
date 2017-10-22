using System;
using System.Security.Cryptography;
using System.Text;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class DpapiDecryptor : IDpapiDecryptor, IRequestDependency
    {
        public string DecryptToString(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
      
            byte[] decryptedBytes = DecryptToBytes(input, optionalEntropy, scope);
            return System.Text.Encoding.UTF8.GetString(decryptedBytes);
        }

        public byte[] DecryptToBytes(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            byte[] decryptedBytes = input;
            try
            {
                decryptedBytes = ProtectedData.Unprotect(input, optionalEntropy, scope);
            }
            catch (CryptographicException)
            {
            }
            return decryptedBytes;
        }
    }
}