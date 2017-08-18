namespace PicnicAuth.Interfaces.Encoding
{
    public interface ILongConverter
    {
        byte[] ConvertToBytesBigEndian(ulong input);
    }
}