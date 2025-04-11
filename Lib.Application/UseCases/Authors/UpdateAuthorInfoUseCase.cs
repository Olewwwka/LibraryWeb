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

        public async Task<Guid> ExecuteAsync(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            var existingAuthor = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(request.Id, cancellationToken);

            if (existingAuthor == null)
            {
                throw new NotFoundException($"Author with id {request.Id} not found");
            }

            var authorToUp = _mapper.Map<AuthorEntity>(request);

            var id = _unitOfWork.AuthorsRepository.UpdateAuthor(authorToUp);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}
