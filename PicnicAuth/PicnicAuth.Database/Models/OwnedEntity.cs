using System;

namespace PicnicAuth.Database.Models
{
    public abstract class OwnedEntity : Entity
    {
        public Guid OwnerId { get; set; }
    }
}