using FluentValidation;
using Lib.API.Contracts;
using Lib.Core.Enums;

namespace Lib.API.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty()
                    .WithMessage("ISBN is required")
                .Length(10, 20)
                    .WithMessage("ISBN must be between 10 and 20 characters");

            RuleFor(book => book.Name)
                .NotEmpty()
                    .WithMessage("Book title is required")
                .MinimumLength(2)
                    .WithMessage("Book title must be at least 2 characters")
                .MaximumLength(50)
                    .WithMessage("Book title must contain no more than 50 characters");

            RuleFor(book => book.Genre)
                .IsInEnum()
                    .WithMessage("Invalid genre");

            RuleFor(book => book.Description)
                .MaximumLength(1000)
                    .WithMessage("Description must contain no more than 1000 characters");

            RuleFor(book => book.AuthorId)
                .NotEmpty()
                    .WithMessage("Author ID is required");
        }
    }
}
