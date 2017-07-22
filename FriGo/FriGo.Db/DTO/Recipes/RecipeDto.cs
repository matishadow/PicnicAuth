using System;
using System.Collections.Generic;
using FriGo.Db.DTO.Account;
using FriGo.Db.DTO.IngredientQuantities;
using FriGo.Db.DTO.RecipeNotes;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.DTO.Recipes
{
    public class RecipeDto : DtoEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public UserStub User { get; set; }
        public decimal? Rating { get; set; }
        public int? UserRating { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Fitness { get; set; }

        public IEnumerable<IngredientQuantityDto> IngredientQuantities { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
        public IEnumerable<RecipeNoteDto> Notes { get; set; }
        public IEnumerable<IngredientQuantityDto> MissingIngredientQuantities { get; set; }
    }
}