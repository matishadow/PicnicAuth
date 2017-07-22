using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.Database.EntityConfigurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
        }
    }
}