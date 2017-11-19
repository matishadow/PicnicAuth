using System.Drawing;
using System.Net.Http;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Web;
using PicnicAuth.Interfaces.Image;

namespace PicnicAuth.Tests.TestFixtures.Web
{
    [TestFixture]
    public class HttpResponseMessageCreatorTest
    {
        [Test]
        public void TestCreatePngResponse()
        {
            var imageConverterMock = new Mock<IImageConverter>();
            imageConverterMock.Setup(converter => converter.PngImageToBytes(It.IsAny<Bitmap>()))
                .Returns(new byte[]
                {
                    137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 10, 0, 0, 0, 10, 8, 2, 0, 0,
                    0,
                    2, 80, 88, 234, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72,
                    89,
                    115, 0, 0, 36, 231, 0, 0, 36, 231, 1, 244, 173, 171, 6, 0, 0, 0, 19, 73, 68, 65, 84, 40, 83, 99,
                    248,
                    143, 23, 140, 112, 105, 6, 172, 128, 129, 1, 0, 207, 59, 18, 252, 84, 201, 193, 139, 0, 0, 0, 0, 73,
                    69,
                    78, 68, 174, 66, 96, 130
                });

            var creator = new HttpResponseMessageCreator(imageConverterMock.Object);

            HttpResponseMessage response = creator.CreatePngResponse(new Bitmap(100, 100));

            Assert.IsNotNull(response);
        }
    }
}