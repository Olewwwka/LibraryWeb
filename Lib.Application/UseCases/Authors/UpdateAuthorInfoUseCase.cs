using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Exceptions;

namespace Lib.Application.UseCases.Authors
{
    public class UpdateAuthorInfoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateAuthorInfoUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(Guid id, string name, string surname, string country, DateTime birthday, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(id, cancellationToken);

            if (author == null)
            {
                throw new NotFoundException($"Author with id {id} not found");
            }

            var authorId = await _unitOfWork.AuthorsRepository.UpdateAuthorAsync(id, name, surname, birthday, country, cancellationToken);

            return authorId;
        }
    }
}
