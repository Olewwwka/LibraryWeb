using Lib.Core.DTOs;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IUsersService
    {
        Task<User> Register(string name, string email, string password, CancellationToken cancellationToken);
        Task<(User, string accessToken, string RefreshToken)> Login(string email, string password, CancellationToken cancellationToken);
        Task<List<Book>> GetUsersBorrowedBooks(Guid userId, CancellationToken cancellationToken);
        Task<Book> ReturnBook(Guid bookId, CancellationToken cancellationToken);
        Task<Book> BorrowBook(Guid userId, Guid bookId, DateTime borrowTime, DateTime returnTime, CancellationToken cancellationToken);
    }
}
