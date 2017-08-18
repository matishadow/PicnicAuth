namespace PicnicAuth.Interfaces.Encoding
{
    public interface IULongConverter
    {
        byte[] ConvertToBytesBigEndian(ulong input);
    }
}