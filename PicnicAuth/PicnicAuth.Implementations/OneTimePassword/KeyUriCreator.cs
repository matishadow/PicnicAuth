using PicnicAuth.Enums;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class KeyUriCreator : IKeyUriCreator
    {
        public string CreateKeyUri(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            throw new System.NotImplementedException();
        }
    }
}