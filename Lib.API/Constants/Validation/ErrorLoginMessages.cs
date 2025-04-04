namespace Lib.API.Constants.Validation
{
    public static class ErrorLoginMessages
    {
        public const string EmailRequired = "Email is required";
        public const string EmailInvalid = "Invalid email format";
        public const string PasswordRequired = "Password is required";
        public const string PasswordMinLength = "Password must be at least 6 characters long";
        public const string PasswordMaxLength = "Password cannot exceed 50 characters";
    }
} 