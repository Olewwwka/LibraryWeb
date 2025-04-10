namespace Lib.Application.Exceptions
{
    public class AuthorAlreadyExistsException : Exception
    {
        public AuthorAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
