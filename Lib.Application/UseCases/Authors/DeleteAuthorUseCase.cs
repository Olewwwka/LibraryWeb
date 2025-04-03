using AutoMapper;
using Lib.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.UseCases.Authors
{
    public class DeleteAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var authorId = await _unitOfWork.AuthorsRepository.DeleteAuthorAsync(id, cancellationToken);

            return authorId;
        }
    }
}
