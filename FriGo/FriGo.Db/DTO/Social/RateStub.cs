using System;
using FriGo.Db.DTO.Account;
using FriGo.Db.DTO.Recipes;

namespace FriGo.Db.DTO.Social
{
    public class RateStub
    {
        public Guid RecipeId { get; set; }
        public int Rating { get; set; }
    }
}