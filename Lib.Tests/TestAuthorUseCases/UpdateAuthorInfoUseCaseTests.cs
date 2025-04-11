using AutoMapper;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Lib.Application.Models;
using Lib.Application.Contracts.Requests;
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

        [Fact]
        public async Task Should_Update_Author_When_Found()
        {
            var id = Guid.NewGuid();
            var request = new UpdateAuthorRequest(
                Id: id,
                Name: "Leha",
                Surname: "Popovich",
                Country: "Russia",
                Birthday: DateTime.Now
            );

            var authorEntity = new AuthorEntity { Id = id };

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(authorEntity);

            _mockMapper.Setup(m => m.Map<AuthorEntity>(request))
                .Returns(authorEntity);

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.UpdateAuthor(authorEntity))
                .Returns(id);

            var result = await _useCase.ExecuteAsync(request, CancellationToken.None);

            Assert.Equal(id, result);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_If_Author_Not_Found()
        {
            var id = Guid.NewGuid();
            var request = new UpdateAuthorRequest(
                Id: id,
                Name: "Leha",
                Surname: "Popovich",
                Country: "Russia",
                Birthday: DateTime.Now
            );

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthorEntity)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _useCase.ExecuteAsync(request, CancellationToken.None));
        }
    }
}
