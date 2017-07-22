using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Db.EntityConfigurations
{
    public class IngredientQuantityConfiguration : EntityTypeConfiguration<IngredientQuantity>
    {
        public IngredientQuantityConfiguration()
        {
            HasRequired(ingredientQuantity => ingredientQuantity.Ingredient);
        }
    }
}