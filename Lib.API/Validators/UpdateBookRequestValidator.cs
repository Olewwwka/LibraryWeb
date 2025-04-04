using FluentValidation;
using Lib.API.Contracts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Lib.API.Constants.Validation;

namespace Lib.API.Validators
{
    public class UpdateBookRequestValidator : AbstractValidator<UpdateBookRequest>
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

            RuleFor(book => book.Genre)
                .NotEmpty().WithMessage(ErrorUpdateBookMessages.GenreRequired);

            RuleFor(book => book.Description)
                .NotEmpty().WithMessage(ErrorUpdateBookMessages.DescriptionRequired)
                .MinimumLength(ValidationConstants.DescriptionMinLength).WithMessage(ErrorUpdateBookMessages.DescriptionMinLength)
                .MaximumLength(ValidationConstants.DescriptionMaxLength).WithMessage(ErrorUpdateBookMessages.DescriptionMaxLength);
        }
    }
}
