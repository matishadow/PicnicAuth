namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface ITotpGenerator
    {
        string GenerateTotp(byte[] secret);
    }
}