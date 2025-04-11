using AutoMapper;
using Lib.Application.Models;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Moq;
using Xunit;

namespace Lib.Tests.TestAuthorUseCases
{
    public class GetAuthorByIdUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAuthorByIdUseCase _useCase;

        public GetAuthorByIdUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new GetAuthorByIdUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Should_Return_Author_When_Found()
        {
            var id = Guid.NewGuid();
            var entity = new AuthorEntity { Id = id, Name = "Leha" };
            var author = new Author("Leha", "Popovich", DateTime.Now, "Russia");

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);
            _mockMapper.Setup(m => m.Map<Author>(entity)).Returns(author);

            var result = await _useCase.ExecuteAsync(id, CancellationToken.None);
            Assert.Equal("Leha", result.Name);
        }

        [Fact]
        public async Task Should_Throw_When_Not_Found()
        {
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthorEntity)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _useCase.ExecuteAsync(id, CancellationToken.None));
        }
    }
}
