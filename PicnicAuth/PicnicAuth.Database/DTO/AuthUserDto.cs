using System;

namespace PicnicAuth.Database.DTO
{
    public class AuthUserDto
    {
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public Uri QrCodeUri { get; set; }
    }
}