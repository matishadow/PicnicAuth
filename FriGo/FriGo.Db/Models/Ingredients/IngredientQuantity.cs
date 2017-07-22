using System;
using FluentValidation.Attributes;
using FriGo.Db.ModelValidators;

namespace FriGo.Db.Models.Ingredients
{
    public class IngredientQuantity : Entity
    {
        public decimal Quantity { get; set; }
        public string Description { get; set; }

        public virtual Guid IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
