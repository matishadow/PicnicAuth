using System;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Base64Decoder : IBase64Decoder
    {
        public string Decode(string encodedData)
        {
            if (encodedData == null) return null;

            byte[] encodedBytes = Convert.FromBase64String(encodedData);
            return System.Text.Encoding.UTF8.GetString(encodedBytes);
        }
    }
}
