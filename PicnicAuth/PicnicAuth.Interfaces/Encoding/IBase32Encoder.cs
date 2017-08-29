namespace PicnicAuth.Interfaces.Encoding
{
    public interface IBase32Encoder
    {
        string Encode(byte[] textBytes);
        string Encode(string plainText);
    }
}