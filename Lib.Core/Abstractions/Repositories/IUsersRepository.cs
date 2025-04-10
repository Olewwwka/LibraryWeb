using Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions.Repositories
{
    public interface IUsersRepository
    {
        Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserEntity> GetUserByIdAsync(Guid id);
        Task AddUserAsync(UserEntity user, CancellationToken cancellationToken);
        Task<List<BookEntity>> GetUserBorrowedBooksAsync(Guid id, CancellationToken cancellationToken);
    }
}
