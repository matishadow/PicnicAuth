using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class CreateIngredientQuantityValidator : AbstractDatabaseValidator<CreateIngredientQuantity>, ICreateIngredientQuantityValidator, IRequestDependency
    {
        public CreateIngredientQuantityValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(createIngredientQuantity => createIngredientQuantity.IngredientId)
                .NotEmpty()
                .WithMessage(string.Format(Properties.Resources.EmptyGenericValidationMessage,
                    nameof(CreateIngredientQuantity.IngredientId)))
                .Must((createIngredientQuantity, guid) => EntityExists<Ingredient>(
                    createIngredientQuantity.IngredientId))
                .WithMessage(Properties.Resources.IngredientQuantityIngredientValidationMessage);

            RuleFor(editIngredientQuantity => editIngredientQuantity.Quantity)
                .GreaterThan(0)
                .WithMessage(Properties.Resources.IngredientQuantityQuantityValidationMessage);

            RuleFor(editIngredientQuantity => editIngredientQuantity.Description)
                .Length(0, 100)
                .WithMessage(Properties.Resources.EditIngredientQuantityDescriptionValidationMessage);
        }
    }
}