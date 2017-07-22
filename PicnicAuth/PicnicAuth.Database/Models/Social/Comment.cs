using System;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.Database.Models.Social
{
    public class Comment : OwnedEntity
    {
        public Comment()
        {
            CreatedAt = DateTime.Now;
        }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ImageId { get; set; }

        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}