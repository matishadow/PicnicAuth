using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Models.Authentication;
using PicnicAuth.ServiceInterfaces.Dependencies;
using PicnicAuth.Validation.Interfaces;

namespace PicnicAuth.Validation.Validators
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
                .Must((model, s) => IsFieldUnique<CompanyAccount>(user => user.Email == model.Email))
                .WithMessage(Properties.Resources.EmailExistsValidationMessage);

            RuleFor(register => register.UserName)
                .NotEmpty()
                .WithMessage(Properties.Resources.LoginEmptyValidationMessage)
                .Must((model, s) => IsFieldUnique<CompanyAccount>(user => user.UserName == model.UserName))
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