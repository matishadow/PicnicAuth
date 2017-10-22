namespace PicnicAuth.Interfaces.Cryptography.Signature
{
    public interface IHmacSha1Generator
    {
        byte[] GenerateHmacSha1Hash(byte[] input, byte[] key);
        byte[] GenerateHmacSha1Hash(long input, byte[] key);
        byte[] GenerateHmacSha1Hash(string input, byte[] key);
    }
}