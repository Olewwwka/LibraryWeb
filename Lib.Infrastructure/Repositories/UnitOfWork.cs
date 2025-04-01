using Lib.Core.Abstractions;
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

        public UnitOfWork(LibraryDbContext dbcontext)
        {
            _dbContext = dbcontext;
            UsersRepository = new UsersRepository(_dbContext);
        }
    }
}
