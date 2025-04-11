using AutoMapper;
using Lib.Application.Models;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Moq;
using Xunit;
using Lib.Application.Contracts.Requests;

namespace Lib.Tests.TestAuthorUseCases
{
    public class GetAuthorBooksUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAuthorBooksUseCase _useCase;

        public GetAuthorBooksUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new GetAuthorBooksUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Should_Return_Books_And_Count()
        {
            var id = Guid.NewGuid();
            var bookEntities = new List<BookEntity> { new BookEntity { Name = "Book1" } };
            var books = new List<Book> { new Book(Guid.NewGuid().ToString(), "Book1", Core.Enums.Genre.Other, "Description", id) };

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthorEntity { Id = id });

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetBooksByAuthorAsync(id, 1, 10, It.IsAny<CancellationToken>()))
                .ReturnsAsync((bookEntities, 1));

            _mockMapper.Setup(m => m.Map<List<Book>>(bookEntities)).Returns(books);

            var request = new GetAllAuthorBooksRequest(id, 1, 10);

            var result = await _useCase.ExecuteAsync(request, CancellationToken.None);

            Assert.Single(result.Books);
            Assert.Equal(1, result.TotalPages);
        }

        [Fact]
        public async Task Should_Throw_When_Author_Not_Found()
        {
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthorEntity)null);
            var request = new GetAllAuthorBooksRequest(id, 1, 10);
            await Assert.ThrowsAsync<NotFoundException>(() => _useCase.ExecuteAsync(request, CancellationToken.None));
        }
    }
}
