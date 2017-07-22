using FluentValidation.Attributes;
using PicnicAuth.Database.ModelValidators;

namespace PicnicAuth.Database.Models.Authentication
{
    [Validator(typeof(RecipeValidator))]
    class RecipeBindingModel
    {
        public string Title { get; set; }
        public string Description { get; set; } 
    }
}
