using System;
using System.Collections.Generic;
using PicnicAuth.Database.Models.Recipes;

namespace PicnicAuth.ServiceInterfaces
{
    public interface IRateService
    {
        IEnumerable<Rate> Get();
        Rate Get(Guid id);

        IEnumerable<Rate> GetByUserId(string userId);
        IEnumerable<Rate> GetByRecipeId(Guid recipeId);
        void Add(Rate rate);
        void Edit(Rate rate);
        void Delete(Guid id);
    }
}
