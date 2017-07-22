using FluentValidation;
using PicnicAuth.Database.DTO.Social;
using PicnicAuth.Database.ModelValidators.Interfaces;
using PicnicAuth.Interfaces.Dependencies;

namespace PicnicAuth.Database.ModelValidators.Validators
{
    public class InputCommentValidator : AbstractContinueValidator<EditComment>, IInputCommentValidator, IRequestDependency
    {
        public InputCommentValidator()
        {
            const int maxCommentLength = 200;
            const int minCommentLength = 0;

            RuleFor(comment => comment.Text)
                .NotEmpty().WithMessage(Properties.Resources.EmptyCommentValidationMessage)
                .Length(minCommentLength, maxCommentLength)
                .WithMessage(string.Format(Properties.Resources.InputCommentValidationMessage, maxCommentLength));
        }
    }
}