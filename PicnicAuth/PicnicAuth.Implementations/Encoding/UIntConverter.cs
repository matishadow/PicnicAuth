using System;
using System.Linq;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class UIntConverter : IUIntConverter
    {
        private const int IntSize = sizeof(int);

        public uint ConvertToIntBigEndian(byte[] bytes)
        {
            if (bytes == null) throw new ArgumentNullException();
            if (bytes.Length > IntSize) throw new ArgumentException();
            if (bytes.Length < IntSize) bytes = PadArrayBigEndian(bytes);

            Array.Reverse(bytes);

            return BitConverter.ToUInt32(bytes, byte.MinValue);
        }

        private byte[] PadArrayBigEndian(byte[] array)
        {
            var resultArray = new byte[IntSize];
            Array.Copy(array, byte.MinValue, resultArray, IntSize - array.Length, array.Length);
            return resultArray;
        }
    }
}