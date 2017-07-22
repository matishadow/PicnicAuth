using System;
using System.Collections.Generic;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.DTO.Social;

namespace PicnicAuth.Database.DTO.Recipes
{
    public class RecipeStub : DtoEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? ImageId { get; set; }

        public IEnumerable<IngredientQuantityDto> IngredientQuantities { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
