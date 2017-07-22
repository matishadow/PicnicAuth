using System.Collections.Generic;
using System.Linq;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class FitnessService : CrudService<IngredientQuantity>, IFitnessService, IRequestDependency
    {
        public FitnessService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<RecipeDto> SortByFitness(decimal minimalFitness, IList<RecipeDto> recipes)
        {
            return recipes
                .Where(recipe => recipe.Fitness >= minimalFitness)
                .OrderByDescending(recipe => recipe.Fitness);
        }

        public decimal CalculateFitness(Recipe recipe, IList<IngredientQuantity> usersIngredientQuantities)
        {
            const int minimalFitness = 0;
            const int maximalFitness = 1;

            if (recipe == null || !recipe.IngredientQuantities.Any() ||
                usersIngredientQuantities == null || !usersIngredientQuantities.Any())
                return minimalFitness;

            var partialFitnesses = new List<decimal>();
            foreach (IngredientQuantity recipeIngredientQuantity in recipe.IngredientQuantities)
            {
                IngredientQuantity usersIngredientQuantity =
                    FindUsersIngredientQuantity(usersIngredientQuantities, recipeIngredientQuantity);

                if (usersIngredientQuantity == null)
                    partialFitnesses.Add(minimalFitness);
                else
                {
                    decimal currentFitness = usersIngredientQuantity.Quantity / recipeIngredientQuantity.Quantity;
                    partialFitnesses.Add(currentFitness > maximalFitness ? maximalFitness : currentFitness);
                }
            }

            return partialFitnesses.Sum() / partialFitnesses.Count;
        }

        public IEnumerable<IngredientQuantity> GetMissingIngredientQuantities(Recipe recipe, IList<IngredientQuantity> usersIngredientQuantities)
        {
            if (recipe == null || !recipe.IngredientQuantities.Any() ||
                usersIngredientQuantities == null)
                return new List<IngredientQuantity>();

            IList<IngredientQuantity> missingIngredientQuantities = new List<IngredientQuantity>();

            foreach (IngredientQuantity recipeIngredientQuantity in recipe.IngredientQuantities)
            {
                IngredientQuantity usersIngredientQuantity =
                    FindUsersIngredientQuantity(usersIngredientQuantities, recipeIngredientQuantity);

                if (usersIngredientQuantity == null)
                    missingIngredientQuantities.Add(recipeIngredientQuantity);
                else if (usersIngredientQuantity.Quantity < recipeIngredientQuantity.Quantity)
                {
                    missingIngredientQuantities.Add(new IngredientQuantity
                    {
                        IngredientId = usersIngredientQuantity.IngredientId,
                        Ingredient =  usersIngredientQuantity.Ingredient,
                        Quantity = recipeIngredientQuantity.Quantity - usersIngredientQuantity.Quantity
                    });
                }
            }

            return missingIngredientQuantities;
        }

        private IngredientQuantity FindUsersIngredientQuantity(IList<IngredientQuantity> usersIngredientQuantities,
            IngredientQuantity recipeIngredientQuantity)
        {
            if (recipeIngredientQuantity == null || 
                usersIngredientQuantities == null || !usersIngredientQuantities.Any())
                return null;

            return usersIngredientQuantities.FirstOrDefault(
                ingredientQuantity => ingredientQuantity.IngredientId == recipeIngredientQuantity.IngredientId);
        }
    }
}
