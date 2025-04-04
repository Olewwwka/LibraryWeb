using FluentValidation;
using Lib.API.Contracts;
using Lib.API.Constants.Validation;

namespace Lib.API.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredISBN)
                .MinimumLength(ValidationConstants.ISBNMinLength).WithMessage(ErrorBookMessages.DescriptionLenght)
                .MaximumLength(ValidationConstants.ISBNMaxLength).WithMessage(ErrorBookMessages.DescriptionLenght);

            RuleFor(book => book.Name)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredTitle)
                .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ErrorBookMessages.DescriptionLenght);

            RuleFor(book => book.Genre)
                .NotEmpty().WithMessage(ErrorBookMessages.InvalidGenre);

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredDescription)
                .MinimumLength(ValidationConstants.DescriptionMinLength).WithMessage(ErrorBookMessages.DescriptionLenght)
                .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage(ErrorBookMessages.DescriptionLenght);

            RuleFor(book => book.AuthorId)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredAuthorId);
        }
    }
}
