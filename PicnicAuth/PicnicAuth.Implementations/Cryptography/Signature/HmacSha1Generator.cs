using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Signature;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Cryptography.Signature
{
    public class HmacSha1Generator : IHmacSha1Generator
    {
        private readonly IULongConverter uLongConverter;
        private readonly IUtf8Converter utf8Converter;

        public HmacSha1Generator(IULongConverter uLongConverter, IUtf8Converter utf8Converter)
        {
            this.uLongConverter = uLongConverter;
            this.utf8Converter = utf8Converter;
        }

        public byte[] GenerateHmacSha1Hash(byte[] input, byte[] key)
        {
            CheckArguments(input, key);
            var hmacsha1 = new HMACSHA1(key);

            return hmacsha1.ComputeHash(input);
        }

        public byte[] GenerateHmacSha1Hash(ulong input, byte[] key)
        {
            CheckArguments(input, key);

            byte[] inputBytes = uLongConverter.ConvertToBytesBigEndian(input);

            var hmacsha1 = new HMACSHA1(key);

            return hmacsha1.ComputeHash(inputBytes);
        }

        public byte[] GenerateHmacSha1Hash(string input, byte[] key)
        {
            CheckArguments(input, key);

            byte[] inputBytes = utf8Converter.ConvertToBytes(input);

            var hmacsha1 = new HMACSHA1(key);

            return hmacsha1.ComputeHash(inputBytes);
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