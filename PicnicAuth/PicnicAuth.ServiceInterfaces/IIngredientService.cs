using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Ingredients;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IIngredientService
    {
        IEnumerable<Ingredient> Get();
        Ingredient Get(Guid id);
        void Add(Ingredient ingredient);
        void Edit(Ingredient ingredient);
        void Delete(Guid id);
    }
}