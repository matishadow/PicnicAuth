using System.Linq;

namespace PicnicAuth.Interfaces.Collections
{
    public interface IArraySlicer
    {
        T[] Slice<T>(T[] array, int beginIndex, int endIndex);
    }
}