namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IHotpVerifier
    {
        bool IsHotpValid(long counter, byte[] secret, string hotp);
    }
}