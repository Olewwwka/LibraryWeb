using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly LibraryDbContext _context;
        public UsersRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
            return user;
        }

        public async Task AddUserAsync(UserEntity user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.AddAsync(user, cancellationToken);
        }

        public async Task<BookEntity> ReturnBookAsync(Guid bookId, CancellationToken cancellationToken)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

            if (book == null)
                throw new KeyNotFoundException("Book not found");

            if (book.UserId == null)
                throw new InvalidOperationException("Book not borrowed");

            book.UserId = null;
            book.BorrowTime = DateTime.MinValue;
            book.ReturnTime = DateTime.MinValue;

            return book;
        }

        public async Task<BookEntity> BorrowBookAsync(
          Guid userId,
          Guid bookId,
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

            return bookEntity;
        }

        public async Task<List<BookEntity>> GetUserBorrowedBooksAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var books = await _context.Books
                .AsNoTracking()
                .Where(book => book.UserId == id)
                .ToListAsync(cancellationToken);
            return books;
        }
    }
}
