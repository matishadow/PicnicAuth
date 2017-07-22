using System;

namespace PicnicAuth.Database.Models.Ingredients
{
    public class Ingredient : OwnedEntity
    {
        public string Name { get; set; }
        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }
    }
}