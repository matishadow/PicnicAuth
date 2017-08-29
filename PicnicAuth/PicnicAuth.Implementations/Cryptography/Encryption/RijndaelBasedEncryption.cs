using System.Security.Cryptography;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public abstract class RijndaelBasedEncryption
    {
        protected int KeySizeInBits => 256;
        protected int KeySizeInBytes => KeySizeInBits / 8;
        protected CryptoStreamMode EncryptorStreamMode = CryptoStreamMode.Read;
        protected int DerivationIterations = 1000;
    }
}