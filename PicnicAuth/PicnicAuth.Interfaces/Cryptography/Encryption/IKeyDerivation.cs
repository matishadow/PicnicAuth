namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IKeyDerivation
    {
        byte[] GetDerivedBytes(byte[] passPhrase, byte[] salt, int iterations, int keySizeInBytes);
    }
}