using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.Models.Authentication;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class RegisterValidator : AbstractDatabaseValidator<RegisterBindingModel>, IRegisterValidator, IRequestDependency
    {
        public RegisterValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(register => register.Email)
                .NotEmpty()
                .WithMessage(Properties.Resources.EmailEmptyValidationMessage)
                .EmailAddress()
                .WithMessage(Properties.Resources.EmailRegexValidationMessage)
                .Must((model, s) => IsFieldUnique<User>(user => user.Email == model.Email))
                .WithMessage(Properties.Resources.EmailExistsValidationMessage);

            RuleFor(register => register.Username)
                .NotEmpty()
                .WithMessage(Properties.Resources.LoginEmptyValidationMessage)
                .Must((model, s) => IsFieldUnique<User>(user => user.UserName == model.Username))
                .WithMessage(Properties.Resources.UsernameExistsValidationMessage);

            const int minimalPasswordLength = 10;
            RuleFor(register => register.Password)
                .NotEmpty()
                .WithMessage(Properties.Resources.CurrentPasswordEmptyValidationMessage)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Properties.Resources.PasswordLengthValidationMessage,
                    minimalPasswordLength));

            RuleFor(register => register.ConfirmPassword)
                .Equal(register => register.Password)
                .WithMessage(Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}