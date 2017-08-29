using System;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Base32Encoder : IBase32Encoder
    {
        public string Encode(byte[] textBytes)
        {
            return textBytes == null ? null : Albireo.Base32.Base32.Encode(textBytes);
        }

        public string Encode(string plainText)
        {
            if (plainText == null) return null;

            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Encode(plainTextBytes);
        }
    }
}
