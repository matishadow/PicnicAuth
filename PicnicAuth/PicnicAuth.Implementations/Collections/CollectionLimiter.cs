using System.Collections.Generic;
using System.Linq;
using PicnicAuth.Interfaces.Collections;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Implementations.Collections
{
    public class CollectionLimiter : ICollectionLimiter, IRequestDependency
    {
        public ICollection<T> Limit<T>(ICollection<T> collection, int page, int count)
        {
            if (collection == null) return new List<T>();

            return collection.Skip((page - 1) * count).Take(count).ToList();
        }
    }
}