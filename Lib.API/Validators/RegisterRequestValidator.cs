using FluentValidation;
using Lib.API.Contracts;

namespace Lib.API.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage("Name is required")
                .MinimumLength(2)
                    .WithMessage("Name must contain at least 5 characters")
                .MaximumLength(20)
                    .WithMessage("Name must contain no more than 50 characters")
                .Matches(@"^[a-zA-Zа-яА-Я]")
                    .WithMessage("Name can only contain letters");

            RuleFor(user => user.Email)
                .NotEmpty()
                    .WithMessage("Email is required.")
                .EmailAddress()
                    .WithMessage("Incorrect email")
                .MaximumLength(50)
                    .WithMessage("Email must contain no more than 50 characters.");

            RuleFor(user => user.Password)
                .NotEmpty()
                    .WithMessage("Password is required.")
                .MinimumLength(8)
                    .WithMessage("Password must contain at least 6 characters.")
                .MaximumLength(30)
                    .WithMessage("Name must contain no more than 30 characters.");

        }
    }
}
