using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using PicnicAuth.Implementations.Collections;
using PicnicAuth.Implementations.Cryptography.Signature;
using PicnicAuth.Interfaces.Collections;
using PicnicAuth.Interfaces.Encoding;

namespace PicnicAuth.Tests
{
    [TestFixture]
    public class ArraySlicerTest
    {
        private static readonly int[] ExampleArray = {1, 2, 3, 4, 5, 6};
        private IArraySlicer slicer;

        [SetUp]
        public void SetUp()
        {
            slicer = new ArraySlicer();
        }

        [TestCase(new[] {1, 2, 3, 4, 5, 6}, 0, 3, ExpectedResult = new[] {1, 2, 3})]
        [TestCase(new[] {1, 2, 3, 4, 5, 6}, 1, 3, ExpectedResult = new[] {2, 3})]
        public T[] TestSlice<T>(T[] array, int beginIndex, int endIndex)
        {
            return slicer.Slice(array, beginIndex, endIndex);
        }

        [Test]
        public void TestSliceNullArray<T>()
        {
            Assert.That(() => slicer.Slice<T>(null, 1, 5),
                Throws.TypeOf<ArgumentNullException>());
        }

        [Test]
        public void TestSliceEndIndexBeforeStartIndex()
        {
            Assert.That(() => slicer.Slice(ExampleArray, 2, 1),
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
