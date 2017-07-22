using System;
using System.Collections.Generic;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Social;

namespace FriGo.Db.Models.Recipes
{
    public class RecipeNote : OwnedEntity
    {
        public RecipeNote()
        {
            CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }

        public Guid RecipeId { get; set; }
    }
}
