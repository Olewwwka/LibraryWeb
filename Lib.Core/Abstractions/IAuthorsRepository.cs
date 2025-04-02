using Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IAuthorsRepository
    {
        Task<List<AuthorEntity>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<AuthorEntity> GetAuthrorByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<AuthorEntity> AddAuthorAsync(AuthorEntity authorEntity, CancellationToken cancellationToken);
        Task<Guid> UpdateAuthorAsync(
            Guid authorId,
            string name,
            string surname,
            DateTime birthday,
            string country,
            CancellationToken cancellationToken);
        Task<Guid> DeleteAuthorAsync(Guid authorId, CancellationToken cancellationToken);
        Task<List<BookEntity>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken);
    }
}
