using System;
using PicnicAuth.Interfaces.Collections;

namespace PicnicAuth.Implementations.Collections
{
    public class ArraySlicer : IArraySlicer
    {
        public T[] Slice<T>(T[] array, int beginIndex, int endIndex)
        {
            if (array == null) throw new ArgumentNullException();

            throw new NotImplementedException();
        }
    }
}