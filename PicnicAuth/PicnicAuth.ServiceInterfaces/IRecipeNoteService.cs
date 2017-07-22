using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IRecipeNoteService
    {
        IEnumerable<RecipeNote> Get();
        RecipeNote Get(Guid recipeNoteId);
        IEnumerable<RecipeNote> Get(Guid recipeId, Guid userId);
        void Add(RecipeNote recipeNote);
        void Edit(RecipeNote recipeNote);
        void Delete(Guid id);
    }
}