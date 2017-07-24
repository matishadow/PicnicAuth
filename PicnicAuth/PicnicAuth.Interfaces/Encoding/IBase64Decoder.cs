namespace PicnicAuth.Interfaces.Encoding
{
    public interface IBase64Decoder
    {
        string Decode(string encodedData);
    }
}