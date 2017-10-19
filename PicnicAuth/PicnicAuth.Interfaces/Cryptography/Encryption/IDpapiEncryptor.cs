using System.Security.Cryptography;

namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IDpapiEncryptor
    {
        byte[] Encrypt(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser);

        byte[] Encrypt(string input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser);
    }
}