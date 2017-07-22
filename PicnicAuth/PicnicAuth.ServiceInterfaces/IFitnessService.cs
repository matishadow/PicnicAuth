using System.Collections.Generic;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IFitnessService
    {
        decimal CalculateFitness(Recipe recipe, IList<IngredientQuantity> usersIngredientQuantities);

        IEnumerable<IngredientQuantity> GetMissingIngredientQuantities(Recipe recipe,
            IList<IngredientQuantity> usersIngredientQuantities);

        IEnumerable<RecipeDto> SortByFitness(decimal minimalFitness, IList<RecipeDto> recipes);
    }
}
