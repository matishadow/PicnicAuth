using System;
using System.Linq;
using PicnicAuth.Interfaces.Collections;

namespace PicnicAuth.Implementations.Collections
{
    public class ArraySlicer : IArraySlicer
    {
        public T[] Slice<T>(T[] array, int beginIndex, int endIndex)
        {
            if (array == null) throw new ArgumentNullException();
            if (beginIndex < byte.MinValue || endIndex >= array.Length) throw new IndexOutOfRangeException();
            if (endIndex <= beginIndex)
                throw new ArgumentException(Properties.Resources.ArraySlicerEndBeforeStartExceptionMessage);

            int itemsToTake = endIndex - beginIndex;

            return array.Skip(beginIndex).Take(itemsToTake).ToArray();
        }
    }
}