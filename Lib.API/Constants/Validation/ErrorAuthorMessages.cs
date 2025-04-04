namespace Lib.API.Constants.Validation
{
    public static class ErrorAuthorMessages
    {
        public const string NameRequired = "Author name is required";
        public const string SurnameRequired = "Author surname is required";
        public const string BirthdayInFuture = "Birthday cannot be in the future";
        public const string BirthdayRequired = "Birthday is required";
        public const string CountryRequired = "Country is required";
        public const string NameMinLength = "Author name must be at least 2 characters long";
        public const string SurnameMinLength = "Author surname must be at least 2 characters long";
        public const string NameMaxLength = "Author name cannot exceed 50 characters";
        public const string SurnameMaxLength = "Author surname cannot exceed 50 characters";
        public const string CountryMaxLength = "Country name cannot exceed 50 characters";
    }
} 