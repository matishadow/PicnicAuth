namespace PicnicAuth.Interfaces.Encoding
{
    public interface IBase32Decoder
    {
        string Decode(string encodedData);
    }
}