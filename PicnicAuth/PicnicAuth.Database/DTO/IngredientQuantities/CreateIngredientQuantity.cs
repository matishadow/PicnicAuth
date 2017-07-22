using System;

namespace PicnicAuth.Database.DTO.IngredientQuantities
{
    public class CreateIngredientQuantity : EditIngredientQuantity
    {
        public Guid IngredientId { get; set; }
    }
}
