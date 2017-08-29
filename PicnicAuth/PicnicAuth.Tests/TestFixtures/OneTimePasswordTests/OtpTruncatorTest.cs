using System;
using Moq;
using NUnit.Framework;
using PicnicAuth.Enums;
using PicnicAuth.Implementations.OneTimePassword;
using PicnicAuth.Interfaces.Collections;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Tests.TestFixtures.OneTimePasswordTests
{
    [TestFixture]
    public class OtpTruncatorTest
    {
        private const int IntSize = sizeof(int);
        private IOtpTruncator truncator;

        [SetUp]
        public void SetUp()
        {
            var arraysOfIntsInBytes = new[]
            {
                new byte[] {122, 9, 81, 235},
                new byte[] {160, 236, 64, 2},
                new byte[] {164, 157, 182, 208},
                new byte[] {87, 169, 166, 30}
            };
            var uInts = new uint[]
            {
                2047431147,
                2699837442,
                2761799376,
                1470735902
            };

            var mockArraySlicer = new Mock<IArraySlicer>();
            mockArraySlicer
                .Setup(slicer => slicer.Slice(new byte[]
                {
                    0x2, 0x73, 0xf5, 0x89, 0x5c, 0x59, 0xd, 0xce, 0xc9, 0x4a,
                    0x7a, 0x9, 0x51, 0xeb, 0x51, 0xb0, 0x1, 0x80, 0xc1, 0x1a
                }, 10, 10 + IntSize))
                .Returns(arraysOfIntsInBytes[0]);
            mockArraySlicer
                .Setup(slicer => slicer.Slice(new byte[]
                {
                    0x6f, 0x94, 0x7e, 0x34, 0x8e, 0xaa, 0xa0, 0xec, 0x40, 0x2,
                    0x5b, 0xd0, 0x4b, 0x6a, 0x53, 0x41, 0x22, 0x48, 0x8f, 0x36
                }, 6, 6 + IntSize))
                .Returns(arraysOfIntsInBytes[1]);
            mockArraySlicer
                .Setup(slicer => slicer.Slice(new byte[]
                {
                    0x6d, 0x15, 0x13, 0xa4, 0x9d, 0xb6, 0xd0, 0xc3, 0xf1, 0xc9,
                    0x75, 0x6, 0x20, 0x3f, 0x2b, 0x2, 0x3d, 0x54, 0xcf, 0x63
                }, 3, 3 + IntSize))
                .Returns(arraysOfIntsInBytes[2]);
            mockArraySlicer
                .Setup(slicer => slicer.Slice(new byte[]
                {
                    0xc9, 0x61, 0xb8, 0x43, 0x67, 0x2c, 0x91, 0xfb, 0xf, 0xd8,
                    0xe8, 0xc5, 0x69, 0x1c, 0x57, 0xa9, 0xa6, 0x1e, 0xcc, 0x7e
                }, 14, 14 + IntSize))
                .Returns(arraysOfIntsInBytes[3]);

            var mockUIntConverter = new Mock<IUIntConverter>();
            mockUIntConverter
                .Setup(converter => converter.ConvertToIntBigEndian(arraysOfIntsInBytes[0]))
                .Returns(uInts[0]);
            mockUIntConverter
                .Setup(converter => converter.ConvertToIntBigEndian(arraysOfIntsInBytes[1]))
                .Returns(uInts[1]);
            mockUIntConverter
                .Setup(converter => converter.ConvertToIntBigEndian(arraysOfIntsInBytes[2]))
                .Returns(uInts[2]);
            mockUIntConverter
                .Setup(converter => converter.ConvertToIntBigEndian(arraysOfIntsInBytes[3]))
                .Returns(uInts[3]);

            truncator = new OtpTruncator(mockArraySlicer.Object, mockUIntConverter.Object);
        }

        [TestCase(new byte[]
        {
            0x6d, 0x15, 0x13, 0xa4, 0x9d, 0xb6, 0xd0, 0xc3, 0xf1, 0xc9,
            0x75, 0x6, 0x20, 0x3f, 0x2b, 0x2, 0x3d, 0x54, 0xcf, 0x63
        }, ExpectedResult = "315728")]
        [TestCase(new byte[]
        {
            0xc9, 0x61, 0xb8, 0x43, 0x67, 0x2c, 0x91, 0xfb, 0xf, 0xd8,
            0xe8, 0xc5, 0x69, 0x1c, 0x57, 0xa9, 0xa6, 0x1e, 0xcc, 0x7e
        }, ExpectedResult = "735902")]
        [TestCase(new byte[]
        {
            0x2, 0x73, 0xf5, 0x89, 0x5c, 0x59, 0xd, 0xce, 0xc9, 0x4a,
            0x7a, 0x9, 0x51, 0xeb, 0x51, 0xb0, 0x1, 0x80, 0xc1, 0x1a
        }, OtpLength.EightDigits, ExpectedResult = "47431147")]
        [TestCase(new byte[]
        {
            0x6f, 0x94, 0x7e, 0x34, 0x8e, 0xaa, 0xa0, 0xec, 0x40, 0x2,
            0x5b, 0xd0, 0x4b, 0x6a, 0x53, 0x41, 0x22, 0x48, 0x8f, 0x36
        }, OtpLength.EightDigits, ExpectedResult = "52353794")]
        public string TestTruncate(byte[] hashSignature, OtpLength otpLength = OtpLength.SixDigits)
        {
            return truncator?.Truncate(hashSignature, otpLength);
        }

        [Test]
        public void TestTruncateNullHashSignature()
        {
            Assert.That(() => truncator.Truncate(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [TestCase(new byte[] { })]
        [TestCase(new byte[] {0, 0, 0, 0, 0})]
        [TestCase(new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})]
        public void TestTruncateInvalidLengthHashSignature(byte[] hashSignature)
        {
            Assert.That(() => truncator.Truncate(hashSignature),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
