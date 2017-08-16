using System;
using PicnicAuth.Interfaces.Time;

namespace PicnicAuth.Implementations.Time
{
    public class UnixTimestampGetter : IUnixTimestampGetter
    {
        public long GetUnixTimestamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }
}