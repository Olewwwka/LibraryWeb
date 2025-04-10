using Lib.Core.Abstractions.Repositories;
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
            return await _context.Books.AsNoTracking().Include(book => book.Author).ToListAsync(cancellationToken);
        }

        public async Task<(List<BookEntity> Books, int TotalCount)> GetPaginatedBooksAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Books
                .AsQueryable()
                .Where(book => book.UserId == null)
                .AsSplitQuery()
                .Include(book =>book.Author);

            var totalCount = await query.CountAsync(cancellationToken);

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (books, totalCount);
        }

        public async Task<BookEntity> GetBookByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(book => book.Id == id, cancellationToken);
        }

        public async Task<(List<BookEntity> Books, int TotalCount)> GetBooksByGenreAsync(Genre genre, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Books
                .AsQueryable()
                .Where(book => book.UserId == null && book.Genre == genre)
                .AsSplitQuery()
                .Include(book => book.Author);

            var totalCount = await query.CountAsync(cancellationToken);

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (books, totalCount);
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

            return book;
        }
        public async Task<string> UpdateBookImageAsync(Guid id, string imagePath, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Books
                .Where(book => book.Id == id)
                .ExecuteUpdateAsync(book => book
                    .SetProperty(book => book.ImagePath, imagePath),
                    cancellationToken);

            return imagePath;
        }

        public async Task<string> DeleteBookImageAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _context.Books
                .Where(book => book.Id == id)
                .ExecuteUpdateAsync(book => book
                    .SetProperty(book => book.ImagePath, "default_image.jpg"),
                    cancellationToken);

            return "default_image.jpg";
        }


        public async Task<BookEntity> UpdateBookAsync(BookEntity bookEntity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var currentBook = await _context.Authors.FindAsync(bookEntity.Id, cancellationToken);
            if (currentBook != null)
            {
                _context.Entry(currentBook).CurrentValues.SetValues(bookEntity);
            }
            else
            {
                _context.Update(bookEntity);
            }

            return bookEntity;
        }

        public async Task<Guid> RemoveBookAsync(Guid id, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(id, cancellationToken);

            if (book != null)
            {
                _context.Books.Remove(book);
            }

            return book.Id;
        }

        public async Task<List<BookEntity>> GetOverdueBooksAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var books = await _context.Books
                .AsNoTracking()
                .Where(book => book.ReturnTime > DateTime.UtcNow)
                .Include(book => book.User)
                .ToListAsync(cancellationToken);
            return books;
        }

        public async Task<(List<BookEntity> Books, int TotalCount)> GetBooksByGenreAndAuthorAsync(Genre genre, Guid authorId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Books
                .AsQueryable()
                .Where(book => book.UserId == null && book.Genre == genre && book.AuthorId == authorId)
                .AsSplitQuery()
                .Include(book => book.Author);

            var totalCount = await query.CountAsync(cancellationToken);

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (books, totalCount);
        }
    }
}
