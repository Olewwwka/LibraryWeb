using Lib.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions.Repositories
{
    public interface IAuthorsRepository
    {
        Task<List<AuthorEntity>> GetAllAuthorsAsync(CancellationToken cancellationToken);
        Task<AuthorEntity> GetAuthrorByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<AuthorEntity> AddAuthorAsync(AuthorEntity authorEntity, CancellationToken cancellationToken);
        Guid UpdateAuthor(AuthorEntity authorEntity);
        Guid RemoveAuthor(AuthorEntity authorEntity);
        Task<(List<BookEntity> Books, int TotalCount)> GetBooksByAuthorAsync(Guid authorId, int pageNumber, int pageSize, CancellationToken cancellationToken);
    }
}
