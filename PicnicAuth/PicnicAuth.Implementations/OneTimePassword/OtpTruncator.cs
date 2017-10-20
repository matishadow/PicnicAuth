using System;
using System.Linq;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Collections;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;
using PicnicAuth.ServiceInterfaces.Dependencies;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class OtpTruncator : IOtpTruncator, IRequestDependency
    {
        private const int ValidSha1SignatureLength = 20;
        private const int TruncateMask = 0b1111;
        private const int OtpBase = 10;

        private readonly IArraySlicer arraySlicer;
        private readonly IUIntConverter uIntConverter;

        public OtpTruncator(IArraySlicer arraySlicer, IUIntConverter uIntConverter)
        {
            this.arraySlicer = arraySlicer;
            this.uIntConverter = uIntConverter;
        }

        public string Truncate(byte[] hmacSignature, OtpLength otpLength = OtpLength.SixDigits)
        {

            if (hmacSignature == null) throw new ArgumentNullException();
            if (hmacSignature.Length != ValidSha1SignatureLength)
                throw new ArgumentException(Properties.Resources.InvalidSha1LengthExceptionMessage);

            int otpDigits = (int)otpLength;

            byte lastByte = hmacSignature.Last();
            int randomStartIndex = lastByte & TruncateMask;
            byte[] resultBytes = arraySlicer.Slice(hmacSignature, randomStartIndex, randomStartIndex + sizeof(int));
            uint resultInt = uIntConverter.ConvertToIntBigEndian(resultBytes);

            resultInt &= int.MaxValue;
            resultInt %= (uint) Math.Pow(OtpBase, otpDigits);

            return resultInt.ToString();
        }
    }
}