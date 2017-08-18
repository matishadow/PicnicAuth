﻿using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Cryptography.Hashing;
using PicnicAuth.Implementations.Cryptography.Signature;
using PicnicAuth.Interfaces.Cryptography.Hashing;
using PicnicAuth.Interfaces.Cryptography.Signature;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class HmacSha1GeneratorTest
    {
        private IHmacSha1Generator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new HmacSha1Generator();
        }

        [TestCase("aaa", new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = new byte[]
            {
                0x14, 0x5a, 0x4e, 0x7a, 0xb2, 0xb3, 0x57, 0xf9, 0xae, 0x58, 0xb2, 0x5f, 0x62, 0x98, 0x52, 0x1b, 0xd9,
                0x3b, 0x1f, 0x30
            })]
        [TestCase("ść", new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = new byte[]
            {
                0x80, 0x4, 0x5a, 0xd2, 0x31, 0xad, 0x5b, 0xe3, 0x3b, 0xc6, 0xbb, 0x29, 0xa1, 0x71, 0xe6, 0x12, 0x82,
                0xe8, 0x0, 0xdd
            })]
        [TestCase("", new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = new byte[]
            {
                0xeb, 0xbf, 0x90, 0x2, 0xb7, 0x3c, 0xc5, 0xb3, 0xae, 0x40, 0x5a, 0x42, 0xe2, 0x82, 0x2a, 0x22, 0x84,
                0x5c, 0x2d, 0x37
            })]
        public byte[] TestComputeStringHash(string input, byte[] key)
        {
            return generator.GenerateHmacSha1Hash(input, key);
        }

        [TestCase(new byte[] {0x61, 0x61, 0x61},
            new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = new byte[]
            {
                0x14, 0x5a, 0x4e, 0x7a, 0xb2, 0xb3, 0x57, 0xf9, 0xae, 0x58, 0xb2, 0x5f, 0x62, 0x98, 0x52, 0x1b, 0xd9,
                0x3b, 0x1f, 0x30
            })]
        [TestCase(new byte[] {0xc5, 0x9b, 0xc4, 0x87}, new byte[]
                {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = new byte[]
            {
                0x80, 0x4, 0x5a, 0xd2, 0x31, 0xad, 0x5b, 0xe3, 0x3b, 0xc6, 0xbb, 0x29, 0xa1, 0x71, 0xe6, 0x12, 0x82,
                0xe8, 0x0, 0xdd
            })]
        [TestCase(new byte[] { }, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef},
            ExpectedResult = new byte[]
            {
                0xeb, 0xbf, 0x90, 0x2, 0xb7, 0x3c, 0xc5, 0xb3, 0xae, 0x40, 0x5a, 0x42, 0xe2, 0x82, 0x2a, 0x22, 0x84,
                0x5c, 0x2d, 0x37
            })]
        public byte[] TestComputeStringHash(byte[] input, byte[] key)
        {
            return generator.GenerateHmacSha1Hash(input, key);
        }

        [TestCase(100, ExpectedResult = new byte[]
        {
            0xd, 0x33, 0xd6, 0xf9, 0x18, 0x9, 0x78, 0x9b, 0x1a, 0x43, 0xf6, 0xcc, 0x9f, 0x6b, 0xa9, 0xbc, 0xc7, 0xd3,
            0x58, 0xdc
        })]
        [TestCase(0, ExpectedResult = new byte[]
        {
            0x91, 0x4e, 0xc, 0x96, 0x34, 0x70, 0xc6, 0xd9, 0x3c, 0x13, 0x10, 0xaa, 0x2a, 0xee, 0x49, 0x88, 0xb1, 0xfb,
            0xf1, 0xb0
        })]
        public byte[] TestComputeStringHash(long input, byte[] key)
        {
            return generator.GenerateHmacSha1Hash(input, key);
        }


        [TestCase(null, new byte[] {0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x21, 0xde, 0xad, 0xbe, 0xef})]
        [TestCase("", null)]
        public void TestNullArgumentGenerateHmacSha1Hash(string input, byte[] key)
        {
            Assert.That(() => generator.GenerateHmacSha1Hash(input, key),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase("", new byte[] { })]
        public void TestEmptyKeyArgumentGenerateHmacSha1Hash(string input, byte[] key)
        {
            Assert.That(() => generator.GenerateHmacSha1Hash(input, key),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
