using System.Collections.Generic;

namespace PicnicAuth.Database.Models.Recipes
{
    public class Tag : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
