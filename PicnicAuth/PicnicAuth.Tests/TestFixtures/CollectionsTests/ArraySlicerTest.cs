using System;
using NUnit.Framework;
using PicnicAuth.Implementations.Collections;
using PicnicAuth.Interfaces.Collections;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class ArraySlicerTest
    {
        private IArraySlicer slicer;

        [SetUp]
        public void SetUp()
        {
            slicer = new ArraySlicer();
        }

        [Test]
        public void TestSlice()
        {
            var exampleArray = new[] {1, 2, 3, 4, 5, 6};
            var resultArray1 = new[] {1, 2, 3};
            var resultArray2 = new[] {2, 3 };

            Assert.AreEqual(slicer.Slice(exampleArray, 0, 3), resultArray1);
            Assert.AreEqual(slicer.Slice(exampleArray, 1, 3), resultArray2);
        }

        [Test]
        public void TestSliceNullArray()
        {
            Assert.That(() => slicer.Slice<int>(null, 1, 5),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestSliceEndIndexBeforeStartIndex()
        {
            Assert.That(() => slicer.Slice(new[] { 1, 2, 3, 4, 5, 6 }, 2, 1),
                Throws.TypeOf<ArgumentException>());
        }

        [TestCase(new[] {1, 2, 3, 4, 5, 6}, -5, 3)]
        [TestCase(new[] {1, 2, 3, 4, 5, 6}, 0, 100)]
        [TestCase(new[] {1, 2, 3, 4, 5, 6}, -1, 100)]
        public void TestSliceIndexesOutOfRange(int[] array, int startIndex, int endIndex)
        {
            Assert.That(() => slicer.Slice(array, startIndex, endIndex),
                Throws.TypeOf<IndexOutOfRangeException>());
        }
    }
}
