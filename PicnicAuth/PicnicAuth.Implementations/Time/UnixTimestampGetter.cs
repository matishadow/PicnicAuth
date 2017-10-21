using System;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Implementations.Time
{
    public class UnixTimestampGetter : IUnixTimestampGetter, IRequestDependency
    {
        public long GetUnixTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}