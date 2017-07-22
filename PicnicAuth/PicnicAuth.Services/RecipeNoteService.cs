using System;
using System.Collections.Generic;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class RecipeNoteService : CrudService<RecipeNote>, IRecipeNoteService, IRequestDependency
    {
        public RecipeNoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IEnumerable<RecipeNote> Get(Guid recipeId, Guid userId)
        {
            return UnitOfWork.Repository<RecipeNote>()
                .Get(recipeNote =>
                    recipeNote.OwnerId == userId &&
                    recipeNote.RecipeId == recipeId);
        }
    }
}