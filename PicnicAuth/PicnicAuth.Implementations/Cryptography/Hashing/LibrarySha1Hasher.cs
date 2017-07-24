using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PicnicAuth.Interfaces.Cryptography.Hashing;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Implementations.Cryptography.Hashing
{
    public class LibrarySha1Hasher : ILibrarySha1Hasher
    {
        private const string HexadecimalFormater = "X2";
        private readonly IUtf8Converter utf8Converter;

        public LibrarySha1Hasher(IUtf8Converter utf8Converter)
        {
            this.utf8Converter = utf8Converter;
        }

        public string ComputeStringHash(string input)
        {
            if (input == null || !input.Any()) return string.Empty;

            using (var sha1 = new SHA1Managed())
            {
                byte[] hash = sha1.ComputeHash(utf8Converter.ConvertToBytes(input));
                var stringBuilder = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                    stringBuilder.Append(b.ToString(HexadecimalFormater));

                return stringBuilder.ToString();
            }
        }
    }
}
