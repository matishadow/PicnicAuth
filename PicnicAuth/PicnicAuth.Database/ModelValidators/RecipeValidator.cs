using FluentValidation;
using PicnicAuth.Database.Models.Authentication;

namespace PicnicAuth.Database.ModelValidators
{
    class RecipeValidator : AbstractValidator<RecipeBindingModel>
    {
        public RecipeValidator()
        {
            RuleFor(register => register.Description)
                .Length(0, 2000)
                .NotEmpty();

            RuleFor(register => register.Title)
                .Length(0, 200)
                .NotEmpty();
        }
    }
}
