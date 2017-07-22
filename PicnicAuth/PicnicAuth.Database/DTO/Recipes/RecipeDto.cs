using System;
using System.Collections.Generic;
using PicnicAuth.Database.DTO.Account;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.DTO.RecipeNotes;
using PicnicAuth.Database.DTO.Social;

namespace PicnicAuth.Database.DTO.Recipes
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