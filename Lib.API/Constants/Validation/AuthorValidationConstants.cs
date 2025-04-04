namespace Lib.API.Constants.Validation
{
    public static class AuthorValidationConstants
    {
        public const int NameMinLength = 2;
        public const int NameMaxLength = 50;
        public const int SurnameMinLength = 2;
        public const int SurnameMaxLength = 50;
        public const int CountryMinLength = 2;
        public const int CountryMaxLength = 60;
        public const int MinBirthYear = 1900;
        public const string NameRegexPattern = @"^[a-zA-Z\s\-']+$";
        public const string SurnameRegexPattern = @"^[a-zA-Z\s\-']+$";
    }
} 