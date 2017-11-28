using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.Interfaces.Validation;
using PicnicAuth.Models.Authentication;

namespace PicnicAuth.Implementations.Validation
{
    public class RegisterValidator : AbstractDatabaseValidator<RegisterBindingModel>, IRegisterValidator, IRequestDependency
    {
        public RegisterValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(register => register.Email)
                .NotEmpty()
                .WithMessage(Database.Properties.Resources.EmailEmptyValidationMessage)
                .EmailAddress()
                .WithMessage(Database.Properties.Resources.EmailRegexValidationMessage)
                .Must((model, s) => IsFieldUnique<CompanyAccount>(user => user.Email == model.Email))
                .WithMessage(Database.Properties.Resources.EmailExistsValidationMessage);

            RuleFor(register => register.UserName)
                .NotEmpty()
                .WithMessage(Database.Properties.Resources.LoginEmptyValidationMessage)
                .Must((model, s) => IsFieldUnique<CompanyAccount>(user => user.UserName == model.UserName))
                .WithMessage(Database.Properties.Resources.UsernameExistsValidationMessage);

            const int minimalPasswordLength = 10;
            RuleFor(register => register.Password)
                .NotEmpty()
                .WithMessage(Database.Properties.Resources.CurrentPasswordEmptyValidationMessage)
                .Length(minimalPasswordLength, 100)
                .WithMessage(string.Format(Database.Properties.Resources.PasswordLengthValidationMessage,
                    minimalPasswordLength));

            RuleFor(register => register.ConfirmPassword)
                .Equal(register => register.Password)
                .WithMessage(Database.Properties.Resources.ConfirmPasswordValidationMessage);
        }
    }
}