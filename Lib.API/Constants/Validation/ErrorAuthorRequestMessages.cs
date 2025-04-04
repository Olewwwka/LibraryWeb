namespace Lib.API.Constants.Validation
{
    public static class ErrorAuthorRequestMessages
    {
        public const string NameRequired = "Name is required";
        public const string NameLength = "Name must be between 2 and 50 characters";
        public const string NameInvalidChars = "Name contains invalid characters";
        public const string SurnameRequired = "Surname is required";
        public const string SurnameLength = "Surname must be between 2 and 50 characters";
        public const string SurnameInvalidChars = "Surname contains invalid characters";
        public const string CountryRequired = "Country is required";
        public const string CountryLength = "Country must be between 2 and 60 characters";
        public const string BirthdayRequired = "Birth date is required";
        public const string BirthdayInPast = "Birth date must be in past";
        public const string BirthdayAfter1900 = "Birth date must be after 1900";
    }
} 