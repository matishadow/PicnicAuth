using FluentValidation.Attributes;
using PicnicAuth.Database.ModelValidators.Validators;

namespace PicnicAuth.Database.Models.Authentication
{
    [Validator(typeof(SetPasswordValidator))]
    public class SetPasswordBindingModel
    {
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
