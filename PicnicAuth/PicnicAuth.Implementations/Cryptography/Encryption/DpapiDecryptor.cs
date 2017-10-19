using System.Security.Cryptography;
using System.Text;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class DpapiDecryptor : IDpapiDecryptor, IRequestDependency
    {
        public string Decrypt(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            byte[] bytes = ProtectedData.Unprotect(input, optionalEntropy, scope);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}