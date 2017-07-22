using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IRecipeService
    {
        ISearchEngine Engine { get; set; }
        IEnumerable<Recipe> Get();
        Recipe Get(Guid id);
        void Add(Recipe recipe);
        void Edit(Recipe recipe);
        void Delete(Guid id);
        decimal? GetRatingByRecipe(Recipe recipe);
        decimal? GetRatingByUser(string userId, Guid recipeId);
        void SetDefaultPicture(Recipe recipe);
    }
}