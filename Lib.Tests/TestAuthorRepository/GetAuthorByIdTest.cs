using FluentAssertions;
using Lib.Core.Entities;
using Lib.Infrastructure;
using Lib.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lib.Tests.TestAuthorRepository
{
    public class GetAuthorByIdTest
    {
        private readonly DbContextOptions<LibraryDbContext> _dbOptions;

        public GetAuthorByIdTest()
        {
            _dbOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetAuthorByIdAsync_ShouldReturnCorrectAuthor()
        {
            var authorId = Guid.NewGuid();

            using (var context = new LibraryDbContext(_dbOptions))
            {
                context.Authors.Add(new AuthorEntity
                {
                    Id = authorId,
                    Name = "Leha",
                    Surname = "Popovich",
                    Country = "Belarus",
                    Birthday = new DateTime(1775, 12, 16)
                });
                await context.SaveChangesAsync();
            }

            using (var context = new LibraryDbContext(_dbOptions))
            {
                var repository = new AuthorsRepository(context);

                var author = await repository.GetAuthrorByIdAsync(authorId, CancellationToken.None);

                author.Should().NotBeNull();
                author!.Name.Should().Be("Leha");
            }
        }
    }
}
