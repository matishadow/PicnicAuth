using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.Models.Social;

namespace PicnicAuth.Database.Models.Recipes
{
    public class Recipe : OwnedEntity
    {
        public Recipe()
        {
            CreatedAt = DateTime.Now;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ImageId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<IngredientQuantity> IngredientQuantities { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
