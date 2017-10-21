using FluentValidation;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Implementations.Validation
{
    public class AbstractContinueValidator<TValidatedEntity> : AbstractValidator<TValidatedEntity>, IRequestDependency
    {
        public AbstractContinueValidator()
        {
            CascadeMode = CascadeMode.Continue;
        }
    }
}