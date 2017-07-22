using System;

namespace FriGo.Db.Models
{
    public abstract class OwnedEntity : Entity
    {
        public Guid OwnerId { get; set; }
    }
}