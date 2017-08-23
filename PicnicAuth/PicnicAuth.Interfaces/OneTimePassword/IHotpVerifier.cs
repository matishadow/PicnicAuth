namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IHotpVerifier
    {
        bool IsHotpValid(ulong counter, byte[] secret, string hotp);
    }
}