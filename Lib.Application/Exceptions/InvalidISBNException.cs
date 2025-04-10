namespace Lib.Application.Exceptions
{
    public class InvalidISBNException : Exception
    {
        public InvalidISBNException(string message) : base(message)
        {
        }
    }
} 