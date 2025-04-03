using FluentValidation;
using Lib.API.Contracts;

namespace Lib.API.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage("Email is required")
                .MaximumLength(254)
                    .WithMessage("Email must be 50 characters or less")
                .EmailAddress()
                    .WithMessage("Valid email required");

            RuleFor(user => user.Password)
                .NotEmpty()
                    .WithMessage("Password is required")
                .MinimumLength(6)
                    .WithMessage("Password must be 6 characters or more")
                .MaximumLength(64)
                    .WithMessage("Password must be 30 characters or less");
        }
    }
}
