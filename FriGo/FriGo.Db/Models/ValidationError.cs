using System.Net;
using System.Web.Http.ModelBinding;
using FluentValidation.Results;

namespace FriGo.Db.Models
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
