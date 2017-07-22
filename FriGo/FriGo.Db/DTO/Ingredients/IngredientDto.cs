using System;
using FriGo.Db.DTO.Units;

namespace FriGo.Db.DTO.Ingredients
{
    public class IngredientDto : DtoEntity
    {
        public string Name { get; set; }
        public virtual UnitDto Unit { get; set; }
    }
}
