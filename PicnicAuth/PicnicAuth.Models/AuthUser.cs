using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Models
{
    public class AuthUser : Entity
    {
        private const int HotpStartingValue = 1;

        public AuthUser()
        {
            HotpCounter = HotpStartingValue;
        }

        public byte[] Secret { get; set; }
        public string ExternalId { get; set; }
        public string UserName { get; set; }
        public long HotpCounter { get; set; }

        public CompanyAccount Company { get; set; }
    }
}
