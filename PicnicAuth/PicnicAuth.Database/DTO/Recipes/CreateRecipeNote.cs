using System;

namespace PicnicAuth.Database.DTO.Recipes
{
    public class CreateRecipeNote : EditRecipeNote
    {
        public Guid RecipeId { get; set; }
    }
}