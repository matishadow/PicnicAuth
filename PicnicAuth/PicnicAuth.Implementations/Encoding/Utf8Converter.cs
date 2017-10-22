using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Utf8Converter : IUtf8Converter, IRequestDependency
    {
        public byte[] ConvertToBytes(string input)
        {
            if (input == null) return null;
            if (string.IsNullOrEmpty(input)) return new byte[]{};

            return System.Text.Encoding.UTF8.GetBytes(input);
        }
    }
}
