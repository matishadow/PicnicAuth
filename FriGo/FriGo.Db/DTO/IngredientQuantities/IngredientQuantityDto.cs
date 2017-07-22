using FriGo.Db.DTO.Ingredients;

namespace FriGo.Db.DTO.IngredientQuantities
{
    public class IngredientQuantityDto : DtoEntity
    {
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public IngredientDto Ingredient { get; set; }
    }
}