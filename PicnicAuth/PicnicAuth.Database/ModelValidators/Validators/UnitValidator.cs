using FluentValidation;
using PicnicAuth.Database.DAL;
using PicnicAuth.Database.DTO.Units;
using PicnicAuth.Database.Models.Ingredients;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class UnitValidator : AbstractDatabaseValidator<CreateUnit>, IUnitValidator, IRequestDependency
    {
        public UnitValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            RuleFor(unit => unit.Name)
                .NotEmpty()
                .WithMessage(Properties.Resources.UnitEmptyValidationMessage)
                .Length(1, 50)
                .WithMessage(Properties.Resources.UnitValidationMessage)
                .Must((createUnit, guid) => IsFieldUnique<Unit>(
                    unit => unit.Name == createUnit.Name))
                .WithMessage(Properties.Resources.UnitExistsValidationMessage);
        }
    }
}