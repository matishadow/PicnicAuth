using System;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using PicnicAuth.Database.SwaggerResponses;

namespace PicnicAuth.Tests.TestFixtures.Database
{
    [TestFixture]
    public class SwaggerAttributesTest
    {
        public void TestConstructors()
        {
            var attributes = new AttributeTestClass();

            Assert.IsNotNull(attributes);
        }
    }
}