using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class RijndaelManagedCreator : IRijndaelManagedCreator
    {
        private const int KeySizeInBits = 256;
        private const CipherMode EncryptorCipherMode = CipherMode.CBC;
        private const PaddingMode EncryptorPaddingMode = PaddingMode.PKCS7;

        public RijndaelManaged CreateRijndaelManaged(int blockSize = KeySizeInBits,
            CipherMode cipherMode = EncryptorCipherMode,
            PaddingMode paddingMode = EncryptorPaddingMode)
        {
            return new RijndaelManaged
            {
                BlockSize = blockSize,
                Mode = cipherMode,
                Padding = paddingMode 
            };
        }
    }
}