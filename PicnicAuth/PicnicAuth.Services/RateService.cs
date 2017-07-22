using System;
using System.Collections.Generic;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    class RateService : CrudService<Rate>, IRateService, IRequestDependency
    {
        public RateService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<Rate> GetByRecipeId(Guid recipeId)
        {
            var rates = UnitOfWork.Repository<Rate>().Get(rate => rate.Recipe.Id == recipeId);
            return rates;
        }

        public IEnumerable<Rate> GetByUserId(string userId)
        {
            var rates = UnitOfWork.Repository<Rate>().Get(rate => rate.User.Id == userId);
            return rates;
        }
    }
}
