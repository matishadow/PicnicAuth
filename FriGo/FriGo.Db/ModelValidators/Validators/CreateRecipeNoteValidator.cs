using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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