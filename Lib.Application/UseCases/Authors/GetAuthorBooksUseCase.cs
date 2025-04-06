using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;
using Lib.Core.Exceptions;

namespace Lib.Application.UseCases.Authors
{
    public class GetAuthorBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<(List<Book> Books, int TotalCount)> ExecuteAsync(Guid id, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(id, cancellationToken);

            if (author == null)
            {
                throw new NotFoundException($"Author with id {id} not found");
            }

            var (bookEntities, totalCount) = await _unitOfWork.AuthorsRepository.GetBooksByAuthorAsync(id, pageNumber, pageSize, cancellationToken);
            var books = _mapper.Map<List<Book>>(bookEntities);

            return (books, totalCount);
        }
    }
}
