namespace Lib.API.Constants.Validation
{
    public static class ErrorUserMessages
    {
        public const string EmailRequired = "Email is required";
        public const string EmailInvalid = "Invalid email format";
        public const string PasswordRequired = "Password is required";
        public const string PasswordMinLength = "Password must be at least 6 characters long";
        public const string PasswordMaxLength = "Password cannot exceed 50 characters";
        public const string NameRequired = "Name is required";
        public const string NameMinLength = "Name must be at least 2 characters long";
        public const string NameMaxLength = "Name cannot exceed 50 characters";
    }
} 