using System;
using System.Collections.Generic;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.DTO.Social;

namespace PicnicAuth.Database.DTO.Recipes
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