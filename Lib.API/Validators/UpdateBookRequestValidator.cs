using FluentValidation;
using Lib.API.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lib.API.Validators
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty()
                    .WithMessage("ISBN is required")
                .Length(10, 20)
                    .WithMessage("ISBN must be 10 to 20 characters");

            RuleFor(book => book.Name)
                .NotEmpty()
                    .WithMessage("Title is required")
                .Length(2, 50)
                    .WithMessage("Title must be 2 to 50 characters");

            RuleFor(book => book.Genre)
                .IsInEnum()
                    .WithMessage("Invalid genre");

            RuleFor(book => book.Description)
                .MaximumLength(1000)
                    .WithMessage("Description must be 1000 characters or less")
                .When(book => !string.IsNullOrEmpty(book.Description));
        }
    }
}
