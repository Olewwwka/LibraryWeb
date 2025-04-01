using Lib.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lib.Infrastructure
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
        }
    }
}
