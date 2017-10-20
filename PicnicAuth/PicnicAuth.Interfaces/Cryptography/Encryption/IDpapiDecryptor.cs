using System.Security.Cryptography;

namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IDpapiDecryptor
    {
        string DecryptToString(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser);

        byte[] DecryptToBytes(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser);
    }
}