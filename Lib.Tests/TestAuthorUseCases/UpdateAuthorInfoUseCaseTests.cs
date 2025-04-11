using AutoMapper;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Lib.Application.Models;
using Moq;
using Xunit;

namespace Lib.Tests.TestAuthorUseCases
{
    public class UpdateAuthorInfoUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UpdateAuthorInfoUseCase _useCase;

        public UpdateAuthorInfoUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new UpdateAuthorInfoUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

       /* [Fact]
        public async Task Should_Update_Author_When_Found()
        {
            var id = Guid.NewGuid();
            var authorModel = new Author("Leha", "Popovich", DateTime.Now, "Russia")
            {
                Id = id
            };

            var authorEntity = new AuthorEntity { Id = id };

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(authorEntity);

            _mockMapper.Setup(m => m.Map<AuthorEntity>(authorModel))
                .Returns(authorEntity);

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.UpdateAuthorAsync(authorEntity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            var result = await _useCase.ExecuteAsync(id, authorModel, CancellationToken.None);

            Assert.Equal(id, result);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_If_Author_Not_Found()
        {
            var id = Guid.NewGuid();
            var authorModel = new Author("Leha", "Popovich", DateTime.Now, "Russia")
            {
                Id = id
            };

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthorEntity)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _useCase.ExecuteAsync(id, authorModel, CancellationToken.None));
        }*/
    }
}
