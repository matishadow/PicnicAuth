using PicnicAuth.Enums;

namespace PicnicAuth.Interfaces.OneTimePassword
{
    public interface IKeyUriCreator
    {
        string CreateKeyUri(string issuer, string user, string secret, OtpType otpType = OtpType.Totp);
    }
}