using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models.Ingredients;

namespace PicnicAuth.Database.EntityConfigurations
{
    public class IngredientConfiguration : EntityTypeConfiguration<Ingredient>
    {
        public IngredientConfiguration()
        {
            Property(ingredient => ingredient.Name).IsRequired();
        }
    }
}