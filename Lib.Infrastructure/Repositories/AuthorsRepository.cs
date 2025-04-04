using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Lib.Infrastructure.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryDbContext _context;
        public AuthorsRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuthorEntity>> GetAllAuthorsAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Authors.ToListAsync(cancellationToken);
        }

        public async Task<AuthorEntity> GetAuthrorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(author => author.Id == id, cancellationToken);
        }

        public async Task<AuthorEntity> AddAuthorAsync(AuthorEntity authorEntity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Authors.AddAsync(authorEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return authorEntity;
        }

        public async Task<Guid> UpdateAuthorAsync(
            Guid authorId,
            string name,
            string surname,
            DateTime birthday,
            string country,
            CancellationToken cancellationToken)

        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Authors
                .Where(author => author.Id == authorId)
                .ExecuteUpdateAsync(author => author
                    .SetProperty(author => author.Name, name)
                    .SetProperty(author => author.Surname, surname)
                    .SetProperty(author => author.Birthday, birthday)
                    .SetProperty(author => author.Country, country),
                    cancellationToken);

            return authorId;
        }

        public async Task<Guid> DeleteAuthorAsync(Guid authorId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Authors
                .Where(author => author.Id == authorId)
                .ExecuteDeleteAsync(cancellationToken);

            return authorId;
        }

        public async Task<List<BookEntity>> GetBooksByAuthorAsync(Guid authorId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .AsNoTracking()
                .Where(book => book.AuthorId == authorId)
                .ToListAsync(cancellationToken);
        }
    }
}
