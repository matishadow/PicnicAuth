using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class Encrypter : IEncrypter
    {
        public byte[] Encrypt(byte[] bytesToEncrypt, byte[] key, byte[] salt, byte[] iv)
        {
            throw new System.NotImplementedException();
        }
    }
}