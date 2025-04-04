using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IUnitOfWork
    {
        IUsersRepository UsersRepository { get; }
        IBooksRepository BooksRepository { get; }
        IAuthorsRepository AuthorsRepository { get; }
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
