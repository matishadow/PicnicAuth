using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
{
    public class AbstractContinueValidator<TValidatedEntity> : AbstractValidator<TValidatedEntity>, IRequestDependency
    {
        public AbstractContinueValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }
    }
}