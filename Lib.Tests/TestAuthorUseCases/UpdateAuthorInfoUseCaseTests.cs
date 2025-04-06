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
            var birthday = DateTime.Now;

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AuthorEntity { Id = id });

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.UpdateAuthorAsync(id, "Leha", "Popovich", birthday, "Russia", It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            var result = await _useCase.ExecuteAsync(id, "Leha", "Popovich", "Russia", birthday, CancellationToken.None);

            Assert.Equal(id, result);
        }

        [Fact]
        public async Task Should_Throw_If_Author_Not_Found()
        {
            var id = Guid.NewGuid();

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthorEntity)null);

            await Assert.ThrowsAsync<NotFoundException>(() =>
                _useCase.ExecuteAsync(id, "Leha", "Popovich", "Russia", DateTime.Now, CancellationToken.None));
        }
    }
}
