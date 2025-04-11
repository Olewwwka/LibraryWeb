namespace Lib.API.Constants.Validation
{
    public class ErrorBookMessages
    {
        public const string RequiredISBN = "ISBN is required";
        public const string RequiredDescription = "Description is required";
        public const string ISBNLenght = "ISBN Lenght must be between 10 and 20 characters";
        public const string RequiredTitle = "Book title is required";
        public const string TitleLenght = "ISBN Lenght must be between 2 and 50 characters";
        public const string InvalidGenre = "Invalid genre";
        public const string DescriptionLenght = "Description must contain no more than 1000 characters";
        public const string RequiredAuthorId = "Author ID is required";
    }
}
