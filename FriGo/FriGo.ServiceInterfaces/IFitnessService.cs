using System;
using System.Collections.Generic;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models.Recipes;
using FriGo.Db.Models.Ingredients;

namespace FriGo.ServiceInterfaces
{
    public interface IFitnessService
    {
        decimal CalculateFitness(Recipe recipe, IList<IngredientQuantity> usersIngredientQuantities);

        IEnumerable<IngredientQuantity> GetMissingIngredientQuantities(Recipe recipe,
            IList<IngredientQuantity> usersIngredientQuantities);

        IEnumerable<RecipeDto> SortByFitness(decimal minimalFitness, IList<RecipeDto> recipes);
    }
}
