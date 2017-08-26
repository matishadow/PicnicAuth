namespace PicnicAuth.Implementations.Cryptography.Encryption
{
    public abstract class RijndaelBasedEncryption
    {
        protected int KeySizeInBits => 256;
        protected int KeySizeInBytes => KeySizeInBits / 8;
    }
}