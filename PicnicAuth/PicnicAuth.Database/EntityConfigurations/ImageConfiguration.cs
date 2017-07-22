using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models;

namespace PicnicAuth.Database.EntityConfigurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
        public ImageConfiguration()
        {
        }
    }
}