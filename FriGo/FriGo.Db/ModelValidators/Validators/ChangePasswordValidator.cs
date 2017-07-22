using FluentValidation;
using FriGo.Db.Models.Authentication;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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