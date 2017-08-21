namespace PicnicAuth.Interfaces.Encoding
{
    public interface IUIntConverter
    {
        uint ConvertToIntBigEndian(byte[] bytes);
    }
}