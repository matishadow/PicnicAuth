using FluentValidation;
using PicnicAuth.Database.DTO.Recipes;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class RateValidator : AbstractContinueValidator<RateRecipe>, IRateValidator, IRequestDependency
    {
        public RateValidator()
        {
            RuleFor(rate => rate.Rate)
                .InclusiveBetween(0, 10)
                .WithMessage(Properties.Resources.RateValidationMessage);
        }
    }
}