using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.Models.Recipes;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class CreateRecipeNoteValidator : AbstractDatabaseValidator<CreateRecipeNote>, ICreateRecipeNoteValidator, IRequestDependency
    {
        public CreateRecipeNoteValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(createRecipeNote => createRecipeNote.RecipeId)
                .NotEmpty()
                .WithMessage(Properties.Resources.RecipeNoteRecipeIdEmptyValidationMessage)
                .Must((note, guid) => EntityExists<Recipe>(note.RecipeId))
                .WithMessage(Properties.Resources.RecipeNoteRecipeExistsValidationMessage);
               

            RuleFor(createRecipeNote => createRecipeNote.Note)
                .NotEmpty()
                .WithMessage(Properties.Resources.RecipeNoteEmptyValidationMessage)
                .Length(0, 200)
                .WithMessage(Properties.Resources.RecipeNoteLengthValidationMessage);
        }
    }
}