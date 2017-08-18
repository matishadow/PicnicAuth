using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Signature;

namespace PicnicAuth.Implementations.Cryptography.Signature
{
    public class HmacSha1Generator : IHmacSha1Generator
    {
        public byte[] GenerateHmacSha1Hash(byte[] input, byte[] key)
        {
            CheckArguments(input, key);
            var hmacsha1 = new HMACSHA1(key);

            return hmacsha1.ComputeHash(input);
        }

        public byte[] GenerateHmacSha1Hash(ulong input, byte[] key)
        {
            CheckArguments(input, key);

            throw new NotImplementedException();
        }

        public byte[] GenerateHmacSha1Hash(string input, byte[] key)
        {
            CheckArguments(input, key);

            throw new NotImplementedException();
        }

        private ArgumentException GenerateEmptyKeyException()
        {
            return new ArgumentException(Properties.Resources.HmacSha1EmptyKeyExceptionMessage);
        }

        private void CheckArguments(object input, byte[] key)
        {
            if (input == null || key == null)
                throw new ArgumentNullException();
            if (!key.Any())
                throw GenerateEmptyKeyException();
        }
    }
}