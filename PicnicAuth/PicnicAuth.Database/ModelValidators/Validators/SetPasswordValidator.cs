using FluentValidation;
using PicnicAuth.Database.Models.Authentication;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class SetPasswordValidator : AbstractValidator<SetPasswordBindingModel>
    {
        public SetPasswordValidator()
        {
            const int minimalPasswordLength = 10;

            RuleFor(setPassword => setPassword.NewPassword)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Properties.Resources.PasswordLengthValidationMessage, minimalPasswordLength))
                .NotEmpty();

            RuleFor(setPassword => setPassword.ConfirmPassword)
                .Matches(setPassword => setPassword.NewPassword)
                .WithMessage(Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}