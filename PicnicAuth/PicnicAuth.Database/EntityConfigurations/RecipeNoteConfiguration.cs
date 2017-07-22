using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.Database.EntityConfigurations
{
    public class RecipeNoteConfiguration : EntityTypeConfiguration<RecipeNote>
    {
        public RecipeNoteConfiguration()
        {
            Property(recipeNote => recipeNote.OwnerId).IsRequired();
            Property(recipeNote => recipeNote.RecipeId).IsRequired();
            Property(recipeNote => recipeNote.Note).IsRequired();
        }
    }
}