namespace PicnicAuth.Interfaces.Cryptography.Encryption
{
    public interface IDecryptor
    {
        byte[] Decrypt(byte[] bytesToDecrypt, byte[] passPhrase, byte[] salt, byte[] iv);
    }
}