using System;
using System.Security.Cryptography;
using PicnicAuth.Interfaces.Cryptography.Randomness;

namespace PicnicAuth.Implementations.Cryptography.Randomness
{
    public class SecureRandomNumberGenerator : ISecureRandomNumberGenerator
    {
        private const int MinimalNumberOfBytes = 1;
        private readonly RNGCryptoServiceProvider cryptoServiceProvider;

        public SecureRandomNumberGenerator()
        {
            cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public byte[] GenerateRandomBytes(int numberOfBytes)
        {
            if (numberOfBytes < MinimalNumberOfBytes || numberOfBytes > sbyte.MaxValue)
                throw new ArgumentOutOfRangeException();

            var buffer = new byte[numberOfBytes];
            cryptoServiceProvider.GetBytes(buffer);
            return buffer;
        }

        public void Dispose()
        {
            cryptoServiceProvider?.Dispose();
        }
    }
}