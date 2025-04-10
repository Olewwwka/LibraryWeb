using AutoMapper;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Exceptions;
using Lib.Core.Entities;
using Lib.Application.Models;
using Lib.Application.Abstractions.Authors;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Authors
{
    public class UpdateAuthorInfoUseCase : IUpdateAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAuthorInfoUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> ExecuteAsync(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            var existingAuthor = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(request.Id, cancellationToken);

            if (existingAuthor == null)
            {
                throw new NotFoundException($"Author with id {request.Id} not found");
            }
            

            var authorEntity = await _unitOfWork.AuthorsRepository
                .UpdateAuthorAsync(_mapper.Map<AuthorEntity>(request), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var author = _mapper.Map<Author>(authorEntity);

            return author;
        }
    }
}
