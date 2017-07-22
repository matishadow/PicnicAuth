namespace PicnicAuth.Database.Models
{
    public class Image : OwnedEntity
    {
        public byte[] ImageBytes { get; set; }
    }
}