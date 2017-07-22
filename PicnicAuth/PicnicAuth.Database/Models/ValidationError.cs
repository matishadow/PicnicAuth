using System.Net;
using FluentValidation.Results;

namespace PicnicAuth.Database.Models
{
    public class ValidationError : Error
    {
        public ValidationError(ValidationResult result)
        {
            ValidationResult = result;
            Code = (int)HttpStatusCode.BadRequest;
        }

        public ValidationResult ValidationResult { get; set; }
    }
}
