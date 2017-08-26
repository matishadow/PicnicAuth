using System.Security.Cryptography;

namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface ICryptoTransformApplier
    {
        byte[] ApplyCryptoTransform(byte[] inputBytes, 
            ICryptoTransform cryptoTransform, CryptoStreamMode streamMode);
    }
}