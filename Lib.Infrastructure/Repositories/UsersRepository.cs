using Lib.Core.Abstractions.Repositories;
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

        public async Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(user => user.BorrowedBooks)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
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
