using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;

namespace Lib.Application.UseCases.Books
{
    public class GetAllBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBooksUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(List<Book> Books, int TotalCount)> ExecuteAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var (bookEntities, totalCount) = await _unitOfWork.BooksRepository.GetPaginatedBooksAsync(pageNumber, pageSize, cancellationToken);
            
            var books = _mapper.Map<List<Book>>( bookEntities);

            return (books, totalCount);

        }
    }
}
