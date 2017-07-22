using System;
using PicnicAuth.Database.DTO.Account;
using PicnicAuth.Database.DTO.Recipes;

namespace PicnicAuth.Database.DTO.Social
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageUrl { get; set; }
        public UserStub User { get; set; }
        public RecipeCommentStub Recipe { get; set; }
    }
}