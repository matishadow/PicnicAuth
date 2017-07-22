using System;
using FriGo.Db.DTO.Account;
using FriGo.Db.DTO.Recipes;

namespace FriGo.Db.DTO.Social
{
    public class CommentStub
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public RecipeStub Recipe { get; set; }
    }
}