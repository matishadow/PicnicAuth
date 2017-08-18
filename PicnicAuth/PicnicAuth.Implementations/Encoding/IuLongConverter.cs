using System;
using System.Linq;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class IULongConverter : Interfaces.Encoding.IULongConverter
    {
        public byte[] ConvertToBytesBigEndian(ulong input)
        {
            byte[] bytes = BitConverter.GetBytes(input);
            Array.Reverse(bytes);

            return bytes;
        }
    }
}