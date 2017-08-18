using PicnicAuth.Interfaces.Cryptography.Signature;

namespace PicnicAuth.Implementations.Cryptography.Signature
{
    public class HmacSha1Generator : IHmacSha1Generator
    {
        public byte[] GenerateHmacSha1Hash(string input, byte[] key)
        {
            throw new System.NotImplementedException();
        }
    }
}