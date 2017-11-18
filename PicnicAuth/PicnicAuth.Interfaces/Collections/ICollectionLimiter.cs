using System.Collections.Generic;

namespace PicnicAuth.Interfaces.Collections
{
    public interface ICollectionLimiter
    {
        ICollection<T> Limit<T>(ICollection<T> collection, int page, int count);
    }
}