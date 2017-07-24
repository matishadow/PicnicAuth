using System;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Base64Encoder : IBase64Encoder
    {
        public string Encode(string plainText)
        {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Encode(plainTextBytes);
        }

        public string Encode(byte[] textBytes)
        {
            return Convert.ToBase64String(textBytes);
        }
    }
}
