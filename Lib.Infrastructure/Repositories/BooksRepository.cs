using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Enums;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Infrastructure.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryDbContext _context;
        public BooksRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookEntity>> GetAllBooksAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<BookEntity> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(book => book.Id == id, cancellationToken);
        }

        public async Task<BookEntity> GetBookByISBNAsync(string isbn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(book => book.ISBN == isbn, cancellationToken);
        }

        public async Task<BookEntity> AddBookAsync(BookEntity book, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Books.AddAsync(book, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return book;
        }

        public async Task<Guid> UpdateBookAsync(Guid id, 
            string isbn, 
            string name, 
            Genre genre, 
            string description, 
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Books
                .Where(book => book.Id == id)
                .ExecuteUpdateAsync(book => book
                    .SetProperty(book => book.ISBN, isbn)
                    .SetProperty(book => book.Name, name)
                    .SetProperty(book => book.Genre, genre)
                    .SetProperty(book => book.Description, description),
                    cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return id;
        }

        public async Task<Guid> DeleteBookAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Books
                .Where(book => book.Id == id)
                .ExecuteDeleteAsync(cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return id;
        }

        public async Task<BookEntity> BorrowBookAsync(
          Guid bookId,
          Guid userId,
          DateTime borrowTime,
          DateTime returnTime,
          CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var bookEntity = await _context.Books
                .FirstOrDefaultAsync(book => book.Id == bookId, cancellationToken);

            if (bookEntity == null)
                throw new KeyNotFoundException("Book not found");

            if (bookEntity.UserId != null)
                throw new InvalidOperationException("Book is already borrowed");

            bookEntity.UserId = userId;
            bookEntity.BorrowTime = borrowTime;
            bookEntity.ReturnTime = returnTime;

            await _context.SaveChangesAsync(cancellationToken);

            return bookEntity;
        }
    }
}
