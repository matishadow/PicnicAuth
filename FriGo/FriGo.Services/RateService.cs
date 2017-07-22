using FriGo.Db.Models.Recipes;
using FriGo.Interfaces.Dependencies;
using FriGo.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriGo.Db.DAL;

namespace FriGo.Services
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
