using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;
using Lib.Core.Enums;

namespace Lib.Application.UseCases.Books
{
    public class GetBooksByGenreUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksByGenreUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(List<Book> Books, int TotalCount)> ExecuteAsync(Genre genre, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var (bookEntities, totalCount) = await _unitOfWork.BooksRepository.GetBooksByGenreAsync(genre, pageNumber, pageSize, cancellationToken);

            var books = _mapper.Map<List<Book>>(bookEntities);

            return (books, totalCount);
        }
    }
}
