using FluentValidation;
using Lib.API.Constants.Validation;
using Lib.API.Contracts;

namespace Lib.API.Validators
{
    public class AuthorRequestValidator : AbstractValidator<UpdateAuthorRequest>
    {
        public AuthorRequestValidator()
        {
            RuleFor(author => author.Name)
                .NotEmpty()
                    .WithMessage(ErrorAuthorRequestMessages.NameRequired)
                .Length(AuthorValidationConstants.NameMinLength, AuthorValidationConstants.NameMaxLength)
                    .WithMessage(ErrorAuthorRequestMessages.NameLength)
                .Matches(AuthorValidationConstants.NameRegexPattern)
                    .WithMessage(ErrorAuthorRequestMessages.NameInvalidChars);

            RuleFor(author => author.Surname)
                .NotEmpty()
                    .WithMessage(ErrorAuthorRequestMessages.SurnameRequired)
                .Length(AuthorValidationConstants.SurnameMinLength, AuthorValidationConstants.SurnameMaxLength)
                    .WithMessage(ErrorAuthorRequestMessages.SurnameLength)
                .Matches(AuthorValidationConstants.SurnameRegexPattern)
                    .WithMessage(ErrorAuthorRequestMessages.SurnameInvalidChars);

            RuleFor(author => author.Country)
                .NotEmpty()
                    .WithMessage(ErrorAuthorRequestMessages.CountryRequired)
                .Length(AuthorValidationConstants.CountryMinLength, AuthorValidationConstants.CountryMaxLength)
                    .WithMessage(ErrorAuthorRequestMessages.CountryLength);

            RuleFor(author => author.Birthday)
                .NotEmpty()
                    .WithMessage(ErrorAuthorRequestMessages.BirthdayRequired)
                .LessThan(DateTime.Today)
                    .WithMessage(ErrorAuthorRequestMessages.BirthdayInPast)
                .GreaterThan(new DateTime(AuthorValidationConstants.MinBirthYear, 1, 1))
                    .WithMessage(ErrorAuthorRequestMessages.BirthdayAfter1900);
        }
    }
}
