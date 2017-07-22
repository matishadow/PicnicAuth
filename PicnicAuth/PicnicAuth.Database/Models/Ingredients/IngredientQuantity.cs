using System;

namespace PicnicAuth.Database.Models.Ingredients
{
    public class IngredientQuantity : Entity
    {
        public decimal Quantity { get; set; }
        public string Description { get; set; }

        public virtual Guid IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
