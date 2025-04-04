namespace Lib.Core.Exceptions
{
    public class BookNotAvailableException : Exception
    {
        public BookNotAvailableException(string message) : base(message)
        {
        }
    }
} 