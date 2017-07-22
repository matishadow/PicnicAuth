using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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