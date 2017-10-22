using System;
using PicnicAuth.Enums;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.OneTimePassword;

namespace PicnicAuth.Implementations.OneTimePassword
{
    public class KeyUriCreator : IKeyUriCreator, IRequestDependency
    {
        private const string KeyUriTemplate = "otpauth://{0}/{1}:{2}?secret={3}&issuer={1}";
        public string CreateKeyUri(string issuer, string user, string secret, OtpType otpType = OtpType.Totp)
        {
            if (AnyArgumentIsNull(issuer, user, secret)) 
                throw new ArgumentNullException();
            if (AnyArgumentIsEmpty(issuer, user, secret))
                throw new ArgumentException();

            return string.Format(KeyUriTemplate, otpType.ToString().ToLower(), issuer, user, secret);
        }

        private bool AnyArgumentIsNull(string issuer, string user, string secret)
        {
            return issuer == null || user == null || secret == null;
        }

        private bool AnyArgumentIsEmpty(string issuer, string user, string secret)
        {
            return string.IsNullOrEmpty(issuer) ||
                   string.IsNullOrEmpty(user) ||
                   string.IsNullOrEmpty(secret);
        }
    }
}