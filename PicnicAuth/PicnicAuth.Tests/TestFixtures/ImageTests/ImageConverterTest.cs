using System;
using System.Drawing;
using System.Drawing.Imaging;
using NUnit.Framework;
using PicnicAuth.Interfaces.Image;
using ImageConverter = PicnicAuth.Implementations.Image.ImageConverter;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class ImageConverterTest
    {
        private IImageConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new ImageConverter();
        }

        [Test]
        public void TestConvertBitmapToPngNullArgument()
        {
            Assert.That(() => converter.ConvertBitmapToPng(null),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestConvertBitmapToPng()
        {
            Bitmap bitmap = CreateDummyBitmap();

            System.Drawing.Image pngImage = converter.ConvertBitmapToPng(bitmap);

            Assert.True(Equals(pngImage.RawFormat, ImageFormat.Png));
        }

        private Bitmap CreateDummyBitmap()
        {
            const int size = 50;

            return new Bitmap(size, size);
        }
    }
}
