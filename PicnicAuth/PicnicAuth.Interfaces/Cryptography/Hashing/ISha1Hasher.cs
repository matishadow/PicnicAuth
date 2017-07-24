namespace PicnicAuth.Interfaces.Cryptography.Hashing
{
    public interface ISha1Hasher
    {
        string ComputeStringHash(string input);
    }
}