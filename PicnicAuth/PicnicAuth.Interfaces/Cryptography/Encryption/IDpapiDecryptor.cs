using System.Security.Cryptography;

namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IDpapiDecryptor
    {
        string Decrypt(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser);
    }
}