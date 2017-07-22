using PicnicAuth.Database.DTO.Ingredients;

namespace PicnicAuth.Database.DTO.IngredientQuantities
{
    public class IngredientQuantityDto : DtoEntity
    {
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public IngredientDto Ingredient { get; set; }
    }
}