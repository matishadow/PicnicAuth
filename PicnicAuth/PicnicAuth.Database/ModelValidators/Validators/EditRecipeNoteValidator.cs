using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class EditRecipeNoteValidator : AbstractDatabaseValidator<EditRecipeNote>, IEditRecipeNoteValidator, IRequestDependency
    {
        public EditRecipeNoteValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(createRecipeNote => createRecipeNote.Note)
                .NotEmpty()
                .WithMessage(Properties.Resources.RecipeNoteEmptyValidationMessage)
                .Length(0, 200)
                .WithMessage(Properties.Resources.RecipeNoteLengthValidationMessage);
        }
    }
}