using FluentValidation;
using Lib.API.Contracts;

namespace Lib.API.Validators
{
    public class AuthorRequestValidator : AbstractValidator<AuthorRequest>
    {
        public AuthorRequestValidator()
        {
            RuleFor(author => author.Name)
                .NotEmpty()
                    .WithMessage("Name is required")
                .Length(2, 50)
                    .WithMessage("Name must be between 2 and 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                     .WithMessage("Name contains invalid characters");

            RuleFor(author => author.Surname)
                .NotEmpty()
                    .WithMessage("Surname is required")
                .Length(2, 50)
                    .WithMessage("Surname must be between 2 and 50 characters")
                .Matches(@"^[a-zA-Z\s\-']+$")
                    .WithMessage("Surname contains invalid characters");

            RuleFor(author => author.Country)
                .NotEmpty()
                    .WithMessage("Country is required")
                .Length(2, 60)
                    .WithMessage("Country must be between 2 and 60 characters");

            RuleFor(author => author.Birthday)
                .NotEmpty()
                    .WithMessage("Birth date is required")
                .LessThan(DateTime.Today)
                    .WithMessage("Birth date must be in past")
                .GreaterThan(new DateTime(1900, 1, 1))
                    .WithMessage("Birth date must be after 1900");
        }
    }
}
