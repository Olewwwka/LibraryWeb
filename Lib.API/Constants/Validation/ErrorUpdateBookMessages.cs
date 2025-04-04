namespace Lib.API.Constants.Validation
{
    public static class ErrorUpdateBookMessages
    {
        public const string ISBNRequired = "ISBN is required";
        public const string ISBNMinLength = "ISBN must be at least 10 characters long";
        public const string ISBNMaxLength = "ISBN cannot exceed 13 characters";
        public const string NameRequired = "Book title is required";
        public const string NameMinLength = "Book title must be at least 2 characters long";
        public const string NameMaxLength = "Book title cannot exceed 50 characters";
        public const string GenreRequired = "Genre is required";
        public const string DescriptionRequired = "Description is required";
        public const string DescriptionMinLength = "Description must be at least 10 characters long";
        public const string DescriptionMaxLength = "Description cannot exceed 1000 characters";
    }
} 