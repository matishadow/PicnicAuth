namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface ISecretGenerator
    {
        byte[] GenerateSecret();
    }
}