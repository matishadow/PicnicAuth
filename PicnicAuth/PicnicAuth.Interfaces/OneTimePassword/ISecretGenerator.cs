namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface ISecretGenerator
    {
        string GenerateSecret();
    }
}