namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IHotpGenerator
    {
        string GenerateHotp(ulong counter, byte[] secret);
    }
}