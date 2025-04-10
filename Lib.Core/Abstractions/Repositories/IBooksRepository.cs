using Lib.Core.Entities;
using Lib.Core.Enums;
using System.Threading;

namespace Lib.Core.Abstractions.Repositories
{
    public interface IBooksRepository
    {
        Task<List<BookEntity>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<(List<BookEntity> Books, int TotalCount)> GetPaginatedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<BookEntity> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BookEntity> GetBookByISBNAsync(string isbn, CancellationToken cancellationToken);
        Task<BookEntity> AddBookAsync(BookEntity book, CancellationToken cancellationToken);
        Task<Guid> UpdateBookAsync(BookEntity bookEntity, CancellationToken cancellationToken);
        Task<Guid> RemoveBookAsync(Guid id, CancellationToken cancellationToken);
        Task<List<BookEntity>> GetOverdueBooksAsync(CancellationToken cancellationToken);
        Task<(List<BookEntity> Books, int TotalCount)> GetBooksByGenreAsync(Genre genre, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<(List<BookEntity> Books, int TotalCount)> GetBooksByGenreAndAuthorAsync(Genre genre, Guid authorId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<string> UpdateBookImageAsync(Guid id, string imagePath, CancellationToken cancellationToken);
        Task<string> DeleteBookImageAsync(Guid id, CancellationToken cancellationToken);
    }
}
