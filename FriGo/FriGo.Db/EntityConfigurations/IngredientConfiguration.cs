using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Db.EntityConfigurations
{
    public class IngredientConfiguration : EntityTypeConfiguration<Ingredient>
    {
        public IngredientConfiguration()
        {
            Property(ingredient => ingredient.Name).IsRequired();
        }
    }
}