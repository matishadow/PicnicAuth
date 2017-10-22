using System;
using System.Linq;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class ULongConverter : IULongConverter, IRequestDependency
    {
        public byte[] ConvertToBytesBigEndian(long input)
        {
            byte[] bytes = BitConverter.GetBytes(input);
            Array.Reverse(bytes);

            return bytes;
        }
    }
}