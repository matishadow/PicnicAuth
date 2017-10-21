using System;

namespace PicnicAuth.Database.DTO
{
    public class AuthUserDto : DtoEntity
    {
        public string ExternalId { get; set; }
        public string UserName { get; set; }

        public Uri HotpQrCodeUri { get; set; }
        public Uri TotpQrCodeUri { get; set; }

        public string SecretInBase32 { get; set; }
    }
}