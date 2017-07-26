using System;

namespace PicnicAuth.Interfaces.Cryptography.Randomness
{
    public interface ISecureRandomNumberGenerator : IDisposable
    {
        byte[] GenerateRandomBytes(int numberOfBytes);
    }
}