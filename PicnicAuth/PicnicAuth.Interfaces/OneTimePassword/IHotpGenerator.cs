namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IHotpGenerator
    {
        string GenerateHotp(long counter, byte[] secret);
    }
}