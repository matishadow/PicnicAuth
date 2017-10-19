namespace PicnicAuth.Database.Models
{
    public class AuthUser : Entity
    {
        public byte[] Secret { get; set; }
        public string ExternalId { get; set; }
    }
}
