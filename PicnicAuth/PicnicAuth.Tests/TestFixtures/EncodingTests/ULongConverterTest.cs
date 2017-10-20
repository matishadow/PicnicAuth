﻿using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;

namespace PicnicAuth.Tests.TestFixtures.EncodingTests
{
    [TestFixture]
    public class ULongConverterTest
    {
        private Interfaces.Encoding.IULongConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new ULongConverter();
        }

        [TestCase(10000U, ExpectedResult = new byte[] {0, 0, 0, 0, 0, 0, 0x27, 0x10})]
        [TestCase(0U, ExpectedResult = new byte[] {0, 0, 0, 0, 0, 0, 0, 0})]
        [TestCase(ulong.MaxValue, 
            ExpectedResult = new byte[] {0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff })]
        public byte[] TestConvertToBytesBigEndian(long input)
        {
            return converter.ConvertToBytesBigEndian(input);
        }
    }
}
