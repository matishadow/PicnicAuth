using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Utf8Converter : IUtf8Converter
    {
        public byte[] ConvertToBytes(string input)
        {
            return System.Text.Encoding.UTF8.GetBytes(input);
        }
    }
}
