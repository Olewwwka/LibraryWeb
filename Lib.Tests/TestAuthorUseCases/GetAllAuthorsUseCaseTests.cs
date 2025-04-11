using AutoMapper;
using FluentAssertions;
using Lib.Application.Models;
using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Entities;
using Moq;
using Xunit;

namespace Lib.Tests.TestAuthorUseCases
{
    public class GetAllAuthorsUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetAllAuthorsUseCase _useCase;

   /*     public GetAllAuthorsUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _useCase = new GetAllAuthorsUseCase(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task Should_Return_All_Authors()
        {
            var entities = new List<AuthorEntity> { new AuthorEntity { Name = "Leha" } };
            var authors = new List<Author> { new Author("Leha", "Popovich", DateTime.Now, "Russia") };

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAllAuthorsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(entities);
            _mockMapper.Setup(m => m.Map<List<Author>>(entities)).Returns(authors);

            var result = await _useCase.ExecuteAsync(CancellationToken.None);
            Assert.Single(result);
        }*/
    }
}
