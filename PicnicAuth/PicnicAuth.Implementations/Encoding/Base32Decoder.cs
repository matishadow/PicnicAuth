using System;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Encoding
{
    public class Base32Decoder : IBase32Decoder
    {
        public string Decode(string encodedData)
        {
            if (encodedData == null) return null;

            byte[] encodedBytes = Albireo.Base32.Base32.Decode(encodedData);
            return System.Text.Encoding.UTF8.GetString(encodedBytes);
        }
    }
}
