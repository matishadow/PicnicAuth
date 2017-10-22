namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface ITotpValidator
    {
        bool IsTotpValid(byte[] secret, string totp);
    }
}