using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Tests.Image;
using QRCoder;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class QrCodeGeneratorTest
    {
        private IQrCodeGenerator generator;

        private IBitmapComparer bitmapComparer;

        [SetUp]
        public void SetUp()
        {
            generator = new QrCodeGenerator();
            bitmapComparer = new BitmapComparer();
        }

        [TearDown]
        public void TearDown()
        {
            generator?.Dispose();
        }

        [Test]
        public void TestGenerateQrCode()
        {
            Bitmap bitmap = generator?.GenerateQrCode("abc", 20, QRCodeGenerator.ECCLevel.M);

            Assert.That(() => bitmapComparer.BitmapsEquals(bitmap, Properties.Resources.Qr_abc_M));
        }

        [Test]
        public void TestNullInputGenerateQrCode()
        {
            Assert.That(() => generator?.GenerateQrCode(null, 20, QRCodeGenerator.ECCLevel.M),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestNegativePixelPerModuleGenerateQrCode()
        {
            Assert.That(() => generator?.GenerateQrCode("a", -20, QRCodeGenerator.ECCLevel.M),
                Throws.TypeOf<ArgumentException>());
        }
    }
}
