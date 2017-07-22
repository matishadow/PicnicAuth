using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.IngredientQuantities;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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