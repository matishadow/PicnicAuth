using System;

namespace PicnicAuth.Database.DTO.RecipeNotes
{
    public class RecipeNoteDto : DtoEntity
    {
        public DateTime CreatedAt { get; set; }
        public string Note { get; set; }

        public Guid RecipeId { get; set; }
    }
}