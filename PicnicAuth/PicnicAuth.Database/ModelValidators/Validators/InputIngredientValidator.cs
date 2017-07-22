using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.Ingredients;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class InputIngredientValidator : AbstractDatabaseValidator<InputIngredient>, IInputIngredientValidator, IRequestDependency
    {
        public InputIngredientValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(createIngredient => createIngredient.Name)
                .NotEmpty()
                .WithMessage(Properties.Resources.IngredientNameEmptyValidationMessage)
                .Length(0, 100)
                .WithMessage(Properties.Resources.IngredientNameLengthValidationMessage);

            RuleFor(createIngredient => createIngredient.UnitId)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage,
                    nameof(CreateIngredient.UnitId)))
                .Must((createIngredient, guid) => EntityExists<Unit>(createIngredient.UnitId))
                .WithMessage(Properties.Resources.IngredientUnitExistMessage)
                .Must((createIngredient, guid) => IsFieldUnique<Ingredient>(
                    ingredient => ingredient.Name == createIngredient.Name &&
                                  ingredient.UnitId == createIngredient.UnitId))
                .WithMessage(Properties.Resources.IngredientNameUniqueValidationMessage);
        }
    }
}