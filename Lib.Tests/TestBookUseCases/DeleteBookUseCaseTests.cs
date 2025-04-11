using Lib.Application.UseCases.Books;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Moq;
using Xunit;

namespace Lib.Tests.TestBookUseCases
{
    public class DeleteBookUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IFileService> _mockFileService;
        private readonly DeleteBookUseCase _useCase;

        public DeleteBookUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockFileService = new Mock<IFileService>();
            _useCase = new DeleteBookUseCase(_mockUnitOfWork.Object, _mockFileService.Object);
        }

        [Fact]
        public async Task Should_Delete_Book_And_Image_When_Found()
        {
            var id = Guid.NewGuid();
            var book = new BookEntity 
            { 
                Id = id,
                ImagePath = "test_image.jpg" 
            };

            _mockUnitOfWork.Setup(u => u.BooksRepository.GetBookByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);
            _mockUnitOfWork.Setup(u => u.BooksRepository.RemoveBookAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            var result = await _useCase.ExecuteAsync(id, CancellationToken.None);

            Assert.Equal(id, result);
            _mockFileService.Verify(f => f.Delete("test_image.jpg"), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Not_Delete_Default_Image()
        {
            var id = Guid.NewGuid();
            var book = new BookEntity 
            { 
                Id = id,
                ImagePath = "default_image.jpg" 
            };

            _mockUnitOfWork.Setup(u => u.BooksRepository.GetBookByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);
            _mockUnitOfWork.Setup(u => u.BooksRepository.RemoveBookAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            var result = await _useCase.ExecuteAsync(id, CancellationToken.None);

            Assert.Equal(id, result);
            _mockFileService.Verify(f => f.Delete(It.IsAny<string>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Throw_If_Book_Not_Found()
        {
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(u => u.BooksRepository.GetBookByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((BookEntity)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _useCase.ExecuteAsync(id, CancellationToken.None));
        }
    }
} 