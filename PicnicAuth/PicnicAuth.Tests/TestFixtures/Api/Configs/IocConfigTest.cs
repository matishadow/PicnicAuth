using NUnit.Framework;
using PicnicAuth.Api.Configs;

namespace PicnicAuth.Tests.TestFixtures.Api.Configs
{
    [TestFixture]
    public class IocConfigTest
    {
        public void TestRun()
        {
            Assert.DoesNotThrow(IocConfig.Run);
        }
    }
}