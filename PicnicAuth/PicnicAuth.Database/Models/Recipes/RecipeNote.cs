using System;

namespace PicnicAuth.Database.Models.Recipes
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
