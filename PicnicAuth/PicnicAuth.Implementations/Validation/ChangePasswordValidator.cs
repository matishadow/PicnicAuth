using FluentValidation;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Validation;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Implementations.Validation
{
    public class ChangePasswordValidator : AbstractContinueValidator<ChangePasswordBindingModel>, IChangePasswordValidator, IRequestDependency
    {
        public ChangePasswordValidator()
        {
            const int minimalPasswordLength = 10;

            RuleFor(changePassword => changePassword.OldPassword)
                .NotEmpty()
                .WithMessage(Database.Properties.Resources.CurrentPasswordEmptyValidationMessage);

            RuleFor(changePassword => changePassword.NewPassword)
                .NotEmpty()
                .WithMessage(Database.Properties.Resources.NewPasswordEmptyValidationMessage)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Database.Properties.Resources.PasswordLengthValidationMessage,
                    minimalPasswordLength));

            RuleFor(changePassword => changePassword.ConfirmPassword)
                .Equal(changePassword => changePassword.NewPassword)
                .WithMessage(Database.Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}