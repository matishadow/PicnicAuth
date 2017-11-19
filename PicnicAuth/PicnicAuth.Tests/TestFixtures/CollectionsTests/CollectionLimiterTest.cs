using System.Collections.Generic;
using NUnit.Framework;
using PicnicAuth.Implementations.Collections;

namespace PicnicAuth.Tests.TestFixtures.CollectionsTests
{
    [TestFixture]
    public class CollectionLimiterTest
    {
        [Test]
        public void TestLimit()
        {
            var collectionLimiter = new CollectionLimiter();
            var collection = new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
            ICollection<int> limited1 = collectionLimiter.Limit(collection, 1, 10);
            ICollection<int> limited2 = collectionLimiter.Limit(collection, 2, 10);

            Assert.AreEqual(limited1.Count, 10);
            Assert.AreEqual(limited2.Count, 1);
        }

        [Test]
        public void TestNullLimit()
        {
            var collectionLimiter = new CollectionLimiter();

            Assert.AreEqual(collectionLimiter.Limit<int>(null, 0, 0), new List<int>());
        }
    }
}