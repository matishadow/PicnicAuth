using FluentValidation;
using PicnicAuth.Database.Models.Ingredients;

namespace PicnicAuth.Database.ModelValidators
{
    public class IngredientQuantityValidator : AbstractValidator<IngredientQuantity>
    {
        public IngredientQuantityValidator()
        {
            RuleFor(ingredientQuantity => ingredientQuantity.Quantity)
                .GreaterThan(0)
                .WithMessage(Properties.Resources.IngredientQuantityQuantityValidationMessage);
        }
    }
}