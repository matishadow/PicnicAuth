namespace PicnicAuth.Interfaces.Encoding
{
    public interface IBase64Encoder
    {
        string Encode(byte[] textBytes);
        string Encode(string plainText);
    }
}