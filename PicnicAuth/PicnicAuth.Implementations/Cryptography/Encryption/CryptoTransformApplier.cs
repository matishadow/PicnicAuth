using System.IO;
using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Encryption;

namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public class CryptoTransformApplier : ICryptoTransformApplier
    {
        public byte[] ApplyCryptoTransform(byte[] inputBytes, 
            ICryptoTransform cryptoTransform, CryptoStreamMode streamMode)
        {
            MemoryStream memoryStream = null;
            CryptoStream cryptoStream = null;

            try
            {
                memoryStream = new MemoryStream();
                cryptoStream = new CryptoStream(memoryStream, cryptoTransform, streamMode);

                cryptoStream.Write(inputBytes, byte.MinValue, inputBytes.Length);
                cryptoStream.FlushFinalBlock();

                byte[] encryptedBytes = memoryStream.ToArray();
                return encryptedBytes;
            }
            finally
            {
                memoryStream?.Dispose();
                cryptoStream?.Dispose();
            }
        }
    }
}