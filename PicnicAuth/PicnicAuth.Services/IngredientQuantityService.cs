using System;
using System.Collections.Generic;
using System.Linq;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class IngredientQuantityService : CrudService<IngredientQuantity>, IIngredientQuantityService, IRequestDependency
    {
        private readonly IUserService userService;


        public IngredientQuantityService(IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork)
        {
            this.userService = userService;
        }

        public IEnumerable<IngredientQuantity> GetByUserId(string userId)
        {
            User user = userService.Get(userId);

            return user?.IngredientQuantities;
        }

        public IngredientQuantity Get(string userId, Guid id)
        {
            IEnumerable<IngredientQuantity> ingredientQuantities = GetByUserId(userId);

            return ingredientQuantities.Single(ingredientQuantity => ingredientQuantity.Id == id);
        }

        public bool IsOwnedByUser(string userId, Guid id)
        {
            User user = userService.Get(userId);

            ICollection<IngredientQuantity> ingredienQuantities = user?.IngredientQuantities;
            return ingredienQuantities != null && ingredienQuantities.Any(ingredientQuantity => ingredientQuantity.Id == id);
        }
    }
}