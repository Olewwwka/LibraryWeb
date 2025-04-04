using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;
using Lib.Core.Exceptions;

namespace Lib.Application.UseCases.Authors
{
    public class GetAuthorByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Author> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var authorEntity = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(id, cancellationToken);

            if (authorEntity == null)
            {
                throw new NotFoundException($"Author with id {id} not found");
            }

            var author = _mapper.Map<Author>(authorEntity);

            return author;
        }
    }
}
