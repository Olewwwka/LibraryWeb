using FluentValidation;
using Lib.API.Contracts;
using Lib.API.Constants.Validation;

namespace Lib.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(ErrorLoginMessages.EmailRequired)
                .EmailAddress().WithMessage(ErrorLoginMessages.EmailInvalid);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ErrorLoginMessages.PasswordRequired)
                .MinimumLength(ValidationConstants.PasswordMinLength).WithMessage(ErrorLoginMessages.PasswordMinLength)
                .MaximumLength(ValidationConstants.PasswordMaxLength).WithMessage(ErrorLoginMessages.PasswordMaxLength);
        }
    }
}
