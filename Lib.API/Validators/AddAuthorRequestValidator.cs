using FluentValidation;
using Lib.API.Constants.Validation;
using Lib.Application.Contracts.Requests;

namespace Lib.API.Validators
{
    public class AddAuthorRequestValidator : AbstractValidator<AddAuthorRequest>
    {
        public AddAuthorRequestValidator()
        {
            RuleFor(author => author.Name)
                .NotEmpty().WithMessage(ErrorAuthorMessages.NameRequired)
                .MinimumLength(ValidationConstants.NameMinLength).WithMessage(ErrorAuthorMessages.NameMinLength)
                .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ErrorAuthorMessages.NameMaxLength);

            RuleFor(author => author.Surname)
                .NotEmpty().WithMessage(ErrorAuthorMessages.SurnameRequired)
                .MinimumLength(ValidationConstants.NameMinLength).WithMessage(ErrorAuthorMessages.SurnameMinLength)
                .MaximumLength(ValidationConstants.NameMaxLength).WithMessage(ErrorAuthorMessages.SurnameMaxLength);

            RuleFor(author => author.Birthday)
                .NotEmpty().WithMessage(ErrorAuthorMessages.BirthdayRequired)
                .LessThan(DateTime.UtcNow).WithMessage(ErrorAuthorMessages.BirthdayInFuture);

            RuleFor(author => author.Country)
                .NotEmpty().WithMessage(ErrorAuthorMessages.CountryRequired)
                .MaximumLength(ValidationConstants.CountryMaxLength).WithMessage(ErrorAuthorMessages.CountryMaxLength);
        }
    }
} 