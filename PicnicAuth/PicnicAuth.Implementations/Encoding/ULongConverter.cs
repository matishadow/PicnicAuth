using System;
using System.Linq;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.ServiceInterfaces.Dependencies;

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