using System;
using NUnit.Framework;

namespace PicnicAuth.Tests.TestFixtures
{
    public class GenericPropertyTest
    {
        protected void TestProperty<TProperty>(Action<TProperty> setter, Func<TProperty> getter)
        {
            TProperty propertyValue = default(TProperty);

            setter(propertyValue);
            TProperty returnedValue = getter();

            Assert.AreEqual(propertyValue, returnedValue);
        }
    }
}