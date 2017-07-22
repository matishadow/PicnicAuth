using FluentValidation;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class ChangePasswordValidator : AbstractContinueValidator<ChangePasswordBindingModel>, IChangePasswordValidator, IRequestDependency
    {
        public ChangePasswordValidator()
        {
            const int minimalPasswordLength = 10;

            RuleFor(changePassword => changePassword.OldPassword)
                .NotEmpty()
                .WithMessage(Properties.Resources.CurrentPasswordEmptyValidationMessage);

            RuleFor(changePassword => changePassword.NewPassword)
                .NotEmpty()
                .WithMessage(Properties.Resources.NewPasswordEmptyValidationMessage)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Properties.Resources.PasswordLengthValidationMessage,
                    minimalPasswordLength));

            RuleFor(changePassword => changePassword.ConfirmPassword)
                .Equal(changePassword => changePassword.NewPassword)
                .WithMessage(Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}