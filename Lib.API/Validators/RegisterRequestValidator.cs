using FluentValidation;
using Lib.API.Contracts;
using Lib.API.Constants.Validation;

namespace Lib.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ErrorUserMessages.EmailRequired)
                .EmailAddress().WithMessage(ErrorUserMessages.EmailInvalid);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ErrorUserMessages.PasswordRequired)
                .MinimumLength(ValidationConstants.PasswordMinLength).WithMessage(ErrorUserMessages.PasswordMinLength)
                .MaximumLength(ValidationConstants.PasswordMaxLength).WithMessage(ErrorUserMessages.PasswordMaxLength);

            RuleFor(user => user.Name)
                .NotEmpty().WithMessage(ErrorUserMessages.NameRequired)
                .MinimumLength(ValidationConstants.NameMinLength).WithMessage(ErrorUserMessages.NameMinLength)
                .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ErrorUserMessages.NameMaxLength);

        }
    }
}
