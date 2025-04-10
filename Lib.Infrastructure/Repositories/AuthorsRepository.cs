using Lib.Core.Abstractions.Repositories;
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

            return await _context.Authors.Include(author => author.Books).ToListAsync(cancellationToken);
        }

        public async Task<AuthorEntity> GetAuthrorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Authors
                .Include(author => author.Books)
                .AsNoTracking()
                .FirstOrDefaultAsync(author => author.Id == id, cancellationToken);
        }

        public async Task<AuthorEntity> AddAuthorAsync(AuthorEntity authorEntity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Authors.AddAsync(authorEntity, cancellationToken);

            return authorEntity;
        }

        public async Task<Guid> UpdateAuthorAsync(
            AuthorEntity authorEntity,
            CancellationToken cancellationToken)

        {
            cancellationToken.ThrowIfCancellationRequested();

            var currentAuthor = await _context.Authors.FindAsync(authorEntity.Id, cancellationToken);
            if (currentAuthor != null)
            {
                _context.Entry(currentAuthor).CurrentValues.SetValues(authorEntity);
            }
            else
            {
                _context.Update(authorEntity);
            }

            return authorEntity.Id;
        }

        public async Task<Guid> RemoveAuthorAsync(Guid authorId, CancellationToken cancellationToken)
        {
            var author = await _context.Authors.FindAsync(authorId, cancellationToken);

            if (author != null)
            {
                _context.Authors.Remove(author);
            }

            return author.Id;
        }

        public async Task<(List<BookEntity> Books, int TotalCount)> GetBooksByAuthorAsync(Guid authorId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = _context.Books
                .AsNoTracking()
                .Include(book => book.Author)
                .Where(book => book.AuthorId == authorId);

            var totalCount = await query.CountAsync(cancellationToken);

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (books, totalCount);
        }
    }
}
