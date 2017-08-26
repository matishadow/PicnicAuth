using System.Security.Cryptography;

namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IRijndaelManagedCreator
    {
        RijndaelManaged CreateRijndaelManaged(int blockSize = 256, 
            CipherMode cipherMode = CipherMode.CBC, 
            PaddingMode paddingMode = PaddingMode.PKCS7);
    }
}