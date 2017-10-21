using PicnicAuth.Database.Models.Authentication;

namespace PicnicAuth.Database.Models
{
    public class AuthUser : Entity
    {
        public AuthUser()
        {
            HotpCounter = 1;
        }

        public byte[] Secret { get; set; }
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public long HotpCounter { get; set; }

        public CompanyAccount Company { get; set; }
    }
}
