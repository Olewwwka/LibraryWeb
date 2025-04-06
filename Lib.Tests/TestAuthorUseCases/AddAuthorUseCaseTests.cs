using AutoMapper;
using FluentAssertions;
using Lib.Application.Models;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Exceptions;
using Moq;
using Xunit;

namespace Lib.Tests.TestAuthorUseCases
{
    public class AddAuthorUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        [Fact]
        public async Task ExecuteAsync_ShouldAddAuthor_WhenAuthorDoesNotExist()
        {
            var authors = new List<AuthorEntity>();
            _unitOfWorkMock.Setup(x => x.AuthorsRepository.GetAllAuthorsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(authors);

            var domainAuthor = new Author("Leha", "Popovich", new DateTime(1828, 9, 9), "Russia");
            var entityAuthor = new AuthorEntity { Name = "Leha", Surname = "Popovich", Country = "Russia", Birthday = domainAuthor.Birthday };

            _mapperMock.Setup(m => m.Map<AuthorEntity>(It.IsAny<Author>())).Returns(entityAuthor);

            var useCase = new AddAuthorUseCase(_unitOfWorkMock.Object, _mapperMock.Object);

            var result = await useCase.ExecuteAsync("Leha", "Popovich", new DateTime(1828, 9, 9), "Russia", CancellationToken.None);

            result.Name.Should().Be("Leha");
            _unitOfWorkMock.Verify(x => x.AuthorsRepository.AddAuthorAsync(It.IsAny<AuthorEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldThrow_WhenAuthorExists()
        {
            var existingAuthors = new List<AuthorEntity>
            {
                new AuthorEntity { Name = "Leha", Surname = "Popovich" }
            };

            _unitOfWorkMock.Setup(x => x.AuthorsRepository.GetAllAuthorsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(existingAuthors);

            var useCase = new AddAuthorUseCase(_unitOfWorkMock.Object, _mapperMock.Object);

            await Assert.ThrowsAsync<AuthorAlreadyExistsException>(() =>
                useCase.ExecuteAsync("Leha", "Popovich", DateTime.Now, "Russia", CancellationToken.None));
        }
    }
}
