using System.Net;
using FluentValidation;
using FluentValidation.Results;
using PicnicAuth.Database.Models;
using PicnicAuth.Interfaces.Dependencies;
using PicnicAuth.ServiceInterfaces;

namespace PicnicAuth.Services
{
    public class ValidatingService : IValidatingService, IRequestDependency
    {
        public bool IsValid<T>(IValidator validator,T entity)
        {
            ValidationResult result = UseValidator(validator, entity);

            return result.IsValid;
        }

        public ValidationError GenerateError<T>(IValidator validator, T entity)
        {
            ValidationResult result = UseValidator(validator, entity);

            return new ValidationError(result);
        }

        public HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }

        private ValidationResult UseValidator<T>(IValidator validator, T entity)
        {
            return validator?.Validate(entity);
        }
    }
}