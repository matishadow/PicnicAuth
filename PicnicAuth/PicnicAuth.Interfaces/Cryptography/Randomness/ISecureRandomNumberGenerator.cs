namespace PicnicAuth.Interfaces.Cryptography.Randomness
{
    public interface ISecureRandomNumberGenerator
    {
        byte[] GenerateRandomBytes(int numberOfBytes);
    }
}