using System;

namespace FriGo.Db.DTO.IngredientQuantities
{
    public class CreateIngredientQuantity : EditIngredientQuantity
    {
        public Guid IngredientId { get; set; }
    }
}
