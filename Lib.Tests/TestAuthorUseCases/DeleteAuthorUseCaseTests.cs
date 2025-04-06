﻿using Lib.Application.UseCases.Authors;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lib.Tests.TestAuthorUseCases
{
    public class DeleteAuthorUseCaseTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteAuthorUseCase _useCase;

        public DeleteAuthorUseCaseTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _useCase = new DeleteAuthorUseCase(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task ShouldDelete_Author_When_Found()
        {
            var id = Guid.NewGuid();
            var entity = new AuthorEntity { Id = id };

            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(entity);
            _mockUnitOfWork.Setup(u => u.AuthorsRepository.DeleteAuthorAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            var result = await _useCase.ExecuteAsync(id, CancellationToken.None);
            Assert.Equal(id, result);
        }

        [Fact]
        public async Task ShouldThrow_If_Not_Found()
        {
            var id = Guid.NewGuid();
            _mockUnitOfWork.Setup(u => u.AuthorsRepository.GetAuthrorByIdAsync(id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((AuthorEntity)null);

            await Assert.ThrowsAsync<NotFoundException>(() => _useCase.ExecuteAsync(id, CancellationToken.None));
        }
    }
}
