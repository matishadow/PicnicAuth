using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.IngredientQuantities;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
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