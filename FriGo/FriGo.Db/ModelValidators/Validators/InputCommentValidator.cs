using FluentValidation;
using FriGo.Db.DAL;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.ModelValidators.Interfaces;
using FriGo.Interfaces.Dependencies;

namespace FriGo.Db.ModelValidators.Validators
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