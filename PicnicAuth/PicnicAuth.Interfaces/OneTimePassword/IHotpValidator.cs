using System;

namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IHotpValidator
    {
        bool IsHotpValid(long counter, byte[] secret, string hotp);
        bool IsHotpValidInWindow(long counter, byte[] secret, string givenHotp, Action<long> setCounter);
    }
}