using PicnicAuth.Database.Models.Authentication;

namespace PicnicAuth.Database.Models
{
    public class AuthUser : Entity
    {
        public byte[] Secret { get; set; }
        public string ExternalId { get; set; }
        public string UserName { get; set; }

        public CompanyAccount Company { get; set; }
    }
}
