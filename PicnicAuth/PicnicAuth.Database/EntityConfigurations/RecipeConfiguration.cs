using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.Database.EntityConfigurations
{
    public class RecipeConfiguration : EntityTypeConfiguration<Recipe>
    {
        public RecipeConfiguration()
        {
            HasMany(recipe => recipe.IngredientQuantities)
                .WithOptional()
                .WillCascadeOnDelete(true);
        }
    }
}