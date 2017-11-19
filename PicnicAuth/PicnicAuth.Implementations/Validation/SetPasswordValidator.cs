using System;
using FluentValidation;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Implementations.Validation
{
    public class SetPasswordValidator : AbstractValidator<SetPasswordBindingModel>
    {
        public SetPasswordValidator()
        {
            const int minimalPasswordLength = 10;

            RuleFor(setPassword => setPassword.NewPassword)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Models.Properties.Resources.PasswordLengthValidationMessage, minimalPasswordLength))
                .NotEmpty();

            RuleFor(setPassword => setPassword.ConfirmPassword)
                .Matches(setPassword => setPassword.NewPassword)
                .WithMessage(Models.Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}