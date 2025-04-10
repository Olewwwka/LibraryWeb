using FluentValidation;
using Lib.API.Constants.Validation;
using Lib.Application.Contracts.Requests;

namespace Lib.API.Validators
{
    public class AddBookRequestValidator : AbstractValidator<AddBookRequest>
    {
        public AddBookRequestValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredISBN)
                .MinimumLength(ValidationConstants.ISBNMinLength).WithMessage(ErrorBookMessages.ISBNLenght)
                .MaximumLength(ValidationConstants.ISBNMaxLength).WithMessage(ErrorBookMessages.ISBNLenght);

            RuleFor(book => book.Name)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredTitle)
                .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ErrorBookMessages.DescriptionLenght);

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredDescription)
                .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage(ErrorBookMessages.DescriptionLenght);

            RuleFor(book => book.AuthorId)
                .NotEmpty().WithMessage(ErrorBookMessages.RequiredAuthorId);
        }
    }
}
