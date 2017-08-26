namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IEncrypter
    {
        byte[] Encrypt(byte[] bytesToEncrypt, byte[] key, byte[] salt, byte[] iv);
    }
}