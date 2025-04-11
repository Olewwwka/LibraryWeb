using AutoMapper;
using FluentAssertions;
using Lib.Application.Models;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Entities;
using Lib.Application.Exceptions;
using Lib.Application.Contracts.Requests;
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
            var request = new AddAuthorRequest(
                Name: "Leha",
                Surname: "Popovich",
                Country: "Russia",
                Birthday: new DateTime(1828, 9, 9)
            );

            var domainAuthor = new Author(request.Name, request.Surname, request.Birthday, request.Country);
            var entityAuthor = new AuthorEntity 
            { 
                Name = request.Name, 
                Surname = request.Surname, 
                Country = request.Country, 
                Birthday = request.Birthday 
            };

            _mapperMock.Setup(m => m.Map<AuthorEntity>(It.Is<Author>(a => 
                a.Name == request.Name && 
                a.Surname == request.Surname && 
                a.Birthday == request.Birthday && 
                a.Country == request.Country)))
                .Returns(entityAuthor);

            _mapperMock.Setup(m => m.Map<Author>(It.Is<AuthorEntity>(a => 
                a.Name == request.Name && 
                a.Surname == request.Surname && 
                a.Birthday == request.Birthday && 
                a.Country == request.Country)))
                .Returns(domainAuthor);

            _unitOfWorkMock.Setup(x => x.AuthorsRepository.AddAuthorAsync(It.IsAny<AuthorEntity>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(entityAuthor);

            var useCase = new AddAuthorUseCase(_unitOfWorkMock.Object, _mapperMock.Object);

            var result = await useCase.ExecuteAsync(request, CancellationToken.None);

            result.Name.Should().Be("Leha");
            _unitOfWorkMock.Verify(x => x.AuthorsRepository.AddAuthorAsync(It.IsAny<AuthorEntity>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
