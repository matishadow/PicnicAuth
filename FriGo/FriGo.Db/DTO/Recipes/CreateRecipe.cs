using System;
using System.Collections.Generic;
using FriGo.Db.DTO.IngredientQuantities;
using FriGo.Db.DTO.Social;

namespace FriGo.Db.DTO.Recipes
{
    public class CreateRecipe
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateIngredientQuantity> CreateIngredientQuantities { get; set; }
        public IEnumerable<CreateTag> Tags { get; set; }        
        public Guid? ImageId { get; set; }
    }
}