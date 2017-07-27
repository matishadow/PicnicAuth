using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using QRCoder;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class QrCodeGeneratorTest
    {
        private QrCodeGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = new QrCodeGenerator();
        }

        [TearDown]
        public void TearDown()
        {
            generator?.Dispose();
        }

        [Test]
        public void TestGenerateQrCode()
        {
            Bitmap b = generator?.GenerateQrCode("abc", 20, QRCodeGenerator.ECCLevel.M);

            Assert.That(() => BitmapsEquals(b, Properties.Resources.Qr_abc_M));
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

        private bool BitmapsEquals(Bitmap bitmap1, Bitmap bitmap2)
        {
            if (!bitmap1.Size.Equals(bitmap2.Size))
                return false;

            for (int x = 0; x < bitmap1.Width; ++x)
                for (int y = 0; y < bitmap1.Height; ++y)
                    if (bitmap1.GetPixel(x, y) != bitmap2.GetPixel(x, y))
                        return false;

            return true;
        }
    }
}
