using FluentValidation;
using Lib.API.Constants.Validation;
using Lib.Application.Contracts.Requests;

namespace Lib.API.Validators
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookInfoRequest>
    {
        public UpdateBookRequestValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage(ErrorUpdateBookMessages.ISBNRequired)
                .MinimumLength(ValidationConstants.ISBNMinLength).WithMessage(ErrorUpdateBookMessages.ISBNMinLength)
                .MaximumLength(ValidationConstants.ISBNMaxLength).WithMessage(ErrorUpdateBookMessages.ISBNMaxLength);

            RuleFor(book => book.Name)
                .NotEmpty().WithMessage(ErrorUpdateBookMessages.NameRequired)
                .MinimumLength(ValidationConstants.NameMinLength).WithMessage(ErrorUpdateBookMessages.NameMinLength)
                .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ErrorUpdateBookMessages.NameMaxLength);

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage(ErrorUpdateBookMessages.DescriptionRequired)
                .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage(ErrorUpdateBookMessages.DescriptionMaxLength);
        }
    }
}
