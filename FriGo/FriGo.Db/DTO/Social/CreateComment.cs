using System;
using FriGo.Db.DTO.Account;

namespace FriGo.Db.DTO.Social
{
    public class CreateComment : EditComment
    {        
        public Guid RecipeId { get; set; }
        public Guid ImageId { get; set; }
    }
}