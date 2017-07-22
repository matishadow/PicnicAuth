using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;

namespace FriGo.Db.EntityConfigurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
        public TagConfiguration()
        {
        }
    }
}