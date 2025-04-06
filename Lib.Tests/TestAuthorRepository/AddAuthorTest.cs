using FluentAssertions;
using Lib.Core.Entities;
using Lib.Infrastructure;
using Lib.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Lib.Tests.TestAuthorRepository
{
    public class AddAuthorTest
    {
        private readonly DbContextOptions<LibraryDbContext> _dbOptions;

        public AddAuthorTest()
        {
            _dbOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddAuthorAsync_ShouldAddAuthorToDatabase()
        {
            using var context = new LibraryDbContext(_dbOptions);
            var repository = new AuthorsRepository(context);

            var author = new AuthorEntity
            {
                Id = Guid.NewGuid(),
                Name = "Leha",
                Surname = "Popovich",
                Country = "Belarus",
                Birthday = new DateTime(1828, 9, 9)
            };

            await repository.AddAuthorAsync(author, CancellationToken.None);

            var result = await context.Authors.FindAsync(author.Id);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Leha");
        }
    }
}
