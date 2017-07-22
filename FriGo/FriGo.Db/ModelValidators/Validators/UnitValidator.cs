using System;
using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.DTO.Units;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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