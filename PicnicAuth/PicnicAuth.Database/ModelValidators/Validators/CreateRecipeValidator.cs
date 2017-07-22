using System.Linq;
using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class CreateRecipeValidator : AbstractDatabaseValidator<CreateRecipe>, 
        ICreateRecipeValidator, IRequestDependency
    {
        public CreateRecipeValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            const int maxDescriptionLength = 2000;

            RuleFor(createRecipe => createRecipe.Title)
                .NotEmpty()
                .WithMessage(Properties.Resources.CreateRecipeTitleEmptyValidationMessage)
                .Length(0, 100)
                .WithMessage(Properties.Resources.CreateRecipeTitleLengthValidationMessage);

            RuleFor(createRecipe => createRecipe.Description)
                .NotEmpty()
                .WithMessage(Properties.Resources.CreateRecipeDescriptionEmptyValidationMessage)
                .Length(0, maxDescriptionLength)
                .WithMessage(string.Format(Properties.Resources.CreateRecipeDescriptionLengthValidationMessage,
                    maxDescriptionLength));

            RuleFor(createRecipe => createRecipe.CreateIngredientQuantities)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithMessage(Properties.Resources.CreateRecipeIngredientsExistsValidationMessage)
                .Must((recipe, quantities) => quantities.All(
                    quantity => EntityExists<Ingredient>(quantity.IngredientId)))
                .WithMessage(Properties.Resources.CreateRecipeIngredientsExistsValidationMessage);
        }
    }
}