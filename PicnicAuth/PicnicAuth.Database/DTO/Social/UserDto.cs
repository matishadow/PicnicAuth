using System;
using System.Collections.Generic;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.DTO.Recipes;

namespace PicnicAuth.Database.DTO.Social
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<CommentStub> Comments { get; set; }
        public IEnumerable<RecipeStub> Recipes { get; set; }
        public IEnumerable<IngredientQuantityDto> IngredientQuantities { get; set; }
        public IEnumerable<RateStub> Rates { get; set; }
    }
}