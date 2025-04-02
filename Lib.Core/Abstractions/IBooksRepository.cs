using Lib.Core.Entities;
using Lib.Core.Enums;

namespace Lib.Core.Abstractions
{
    public interface IBooksRepository
    {
        Task<List<BookEntity>> GetAllBooksAsync(CancellationToken cancellationToken);
        Task<BookEntity> GetBookByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<BookEntity> GetBookByISBNAsync(string isbn, CancellationToken cancellationToken);
        Task<BookEntity> AddBookAsync(BookEntity book, CancellationToken cancellationToken);
        Task<Guid> UpdateBookAsync(Guid id,
            string isbn,
            string name,
            Genre genre,
            string description,
            CancellationToken cancellationToken);
        Task<Guid> DeleteBookAsync(Guid id, CancellationToken cancellationToken);
        Task<BookEntity> BorrowBookAsync(
          Guid bookId,
          Guid userId,
          DateTime borrowTime,
          DateTime returnTime,
          CancellationToken cancellationToken);

    }
}
