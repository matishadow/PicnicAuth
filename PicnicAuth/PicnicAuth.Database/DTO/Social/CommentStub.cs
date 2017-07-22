using System;
using PicnicAuth.Database.DTO.Recipes;

namespace PicnicAuth.Database.DTO.Social
{
    public class CommentStub
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public RecipeStub Recipe { get; set; }
    }
}