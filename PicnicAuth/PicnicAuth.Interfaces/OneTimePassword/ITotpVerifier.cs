namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface ITotpVerifier
    {
        bool IsTotpValid(byte[] secret, string totp);
    }
}