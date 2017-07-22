using System;

namespace PicnicAuth.Database.DTO.Social
{
    public class CreateComment : EditComment
    {        
        public Guid RecipeId { get; set; }
        public Guid ImageId { get; set; }
    }
}