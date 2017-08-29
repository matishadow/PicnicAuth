using System;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Base64Encoder : IBase64Encoder
    {
        public string Encode(string plainText)
        {
            if (plainText == null) return null;

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Encode(plainTextBytes);
        }

        public string Encode(byte[] textBytes)
        {
            return textBytes == null ? null : Convert.ToBase64String(textBytes);
        }
    }
}
