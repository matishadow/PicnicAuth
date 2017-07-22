using FluentValidation;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models.Authentication;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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