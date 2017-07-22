using PicnicAuth.Database.DTO.Units;

namespace PicnicAuth.Database.DTO.Ingredients
{
    public class IngredientDto : DtoEntity
    {
        public string Name { get; set; }
        public virtual UnitDto Unit { get; set; }
    }
}
