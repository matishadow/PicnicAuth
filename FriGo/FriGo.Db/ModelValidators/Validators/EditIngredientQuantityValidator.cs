using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.IngredientQuantities;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
{
    public class EditIngredientQuantityValidator : AbstractDatabaseValidator<EditIngredientQuantity>, IEditIngredientQuantityValidator, IRequestDependency
    {
        public EditIngredientQuantityValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(editIngredientQuantity => editIngredientQuantity.Quantity)
                .GreaterThan(0)
                .WithMessage(Properties.Resources.IngredientQuantityQuantityValidationMessage);

            RuleFor(editIngredientQuantity => editIngredientQuantity.Description)
                .Length(0, 100)
                .WithMessage(Properties.Resources.EditIngredientQuantityDescriptionValidationMessage);
        }
    }
}