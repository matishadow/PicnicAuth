namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IEncryptor
    {
        byte[] Encrypt(byte[] bytesToEncrypt, byte[] passPhrase, byte[] salt, byte[] iv);
    }
}