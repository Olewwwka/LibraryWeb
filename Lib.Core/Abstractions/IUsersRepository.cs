using Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserEntity> GetUserByIdAsync(Guid id);
        Task AddUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<BookEntity> ReturnBookAsync(Guid bookId, CancellationToken cancellationToken);
        Task<BookEntity> BorrowBookAsync(
         Guid bookId,
         Guid userId,
         DateTime borrowTime,
         DateTime returnTime,
         CancellationToken cancellationToken);
        Task<List<BookEntity>> GetUserBorrowedBooksAsync(Guid id, CancellationToken cancellationToken);
    }
}
