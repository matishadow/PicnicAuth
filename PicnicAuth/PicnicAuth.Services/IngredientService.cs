using System.Collections.Generic;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class IngredientService : CrudService<Ingredient>, IIngredientService, IRequestDependency
    {
        public IngredientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IEnumerable<Ingredient> Get()
        {

            return base.Get();
        }
    }
}