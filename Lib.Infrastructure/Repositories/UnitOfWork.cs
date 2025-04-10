using Lib.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public LibraryDbContext _dbContext;
        public IUsersRepository UsersRepository { get; }
        public IBooksRepository BooksRepository { get; }
        public IAuthorsRepository AuthorsRepository { get; }
        public UnitOfWork(
            LibraryDbContext dbcontext,
            IAuthorsRepository AuthorsRepository,
            IUsersRepository UsersRepository,
            IBooksRepository BooksRepository)
        {
           _dbContext = dbcontext;
            this.UsersRepository = UsersRepository;
            this.AuthorsRepository = AuthorsRepository;
            this.BooksRepository = BooksRepository;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
