using System.Security.Cryptography;
using System.Text;
using PicnicAuth.Interfaces.Cryptography.Encryption;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class DpapiEncryptor : IDpapiEncryptor, IRequestDependency
    {
        public byte[] Encrypt(byte[] input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            return ProtectedData.Protect(input, optionalEntropy, scope);
        }

        public byte[] Encrypt(string input, byte[] optionalEntropy = null,
            DataProtectionScope scope = DataProtectionScope.CurrentUser)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);

            return Encrypt(bytes, optionalEntropy, scope);
        }
    }
}