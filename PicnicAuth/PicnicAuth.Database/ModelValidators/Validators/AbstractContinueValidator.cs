using FluentValidation;
using FriGo.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class AbstractContinueValidator<TValidatedEntity> : AbstractValidator<TValidatedEntity>, IRequestDependency
    {
        public AbstractContinueValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }
    }
}