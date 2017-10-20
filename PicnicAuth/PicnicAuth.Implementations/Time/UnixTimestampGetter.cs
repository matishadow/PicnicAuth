using System;
using PicnicAuth.Interfaces.Time;
using PicnicAuth.ServiceInterfaces.Dependencies;

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