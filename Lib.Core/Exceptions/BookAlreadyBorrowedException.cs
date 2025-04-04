namespace Lib.Core.Exceptions
{
    public class BookAlreadyBorrowedException : Exception
    {
        public BookAlreadyBorrowedException(string message) : base(message)
        {
        }
    }
} 