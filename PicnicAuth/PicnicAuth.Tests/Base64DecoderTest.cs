﻿using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class Base64DecoderTest
    {
        private IBase64Decoder decoder;

        [SetUp]
        public void SetUp()
        {
            decoder = new Base64Decoder();
        }

        [TestCase("YWFhYWFh", ExpectedResult = "aaaaaa")]
        [TestCase("xIXEh8W6xbw=", ExpectedResult = "ąćźż")]
        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        public string TestDecode(string encodedData)
        {
            return decoder.Decode(encodedData);
        }
    }
}
