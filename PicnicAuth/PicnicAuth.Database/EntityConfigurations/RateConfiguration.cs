using System.Data.Entity.ModelConfiguration;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.Database.EntityConfigurations
{
    class RateConfiguration : EntityTypeConfiguration<Rate>
    {
        public RateConfiguration()
        {
            HasRequired(rate => rate.User) 
                    .WithMany(user => user.Rates);

            HasRequired(rate => rate.Recipe)
                    .WithMany(recipe => recipe.Rates);
        }
    }
}
