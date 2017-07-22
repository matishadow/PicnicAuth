using System;

namespace FriGo.Db.DTO.Recipes
{
    public class CreateRecipeNote : EditRecipeNote
    {
        public Guid RecipeId { get; set; }
    }
}