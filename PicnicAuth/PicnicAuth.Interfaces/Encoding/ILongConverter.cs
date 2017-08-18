namespace PicnicAuth.Interfaces.Encoding
{
    public interface ILongConverter
    {
        byte[] ConvertToBytes(long input);
    }
}