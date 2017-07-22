using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models.Ingredients;

namespace PicnicAuth.Database.EntityConfigurations
{
    public class IngredientQuantityConfiguration : EntityTypeConfiguration<IngredientQuantity>
    {
        public IngredientQuantityConfiguration()
        {
            HasRequired(ingredientQuantity => ingredientQuantity.Ingredient);
        }
    }
}