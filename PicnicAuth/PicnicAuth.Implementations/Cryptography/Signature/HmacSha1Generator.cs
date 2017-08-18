using System;
using System.Linq;
using PicnicAuth.Interfaces.Cryptography.Signature;

namespace PicnicAuth.Implementations.Cryptography.Signature
{
    public class HmacSha1Generator : IHmacSha1Generator
    {
        public byte[] GenerateHmacSha1Hash(string input, byte[] key)
        {
            if (input == null || key == null) 
                throw new ArgumentNullException();
            if (!key.Any())
                throw new ArgumentException(Properties.Resources.HmacSha1EmptyKeyExceptionMessage);

            throw new NotImplementedException();
        }
    }
}