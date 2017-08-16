using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Encoding;
using PicnicAuth.Implementations.Time;
using PicnicAuth.Interfaces.Encoding;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class UnixTimestampGetterTest
    {
        private IUnixTimestampGetter unixTimestampGetter;

        [SetUp]
        public void SetUp()
        {
            unixTimestampGetter = new UnixTimestampGetter();
        }

        [Test]
        public void TestGetUnixTimestamp()
        {
            long currentTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

            Assert.AreEqual(currentTimestamp, unixTimestampGetter.GetUnixTimestamp());
        }
    }
}
