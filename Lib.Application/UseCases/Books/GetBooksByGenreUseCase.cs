using AutoMapper;
using Lib.Application.Models;
using Lib.Core.Enums;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Responses;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Books
{
    public class GetBooksByGenreUseCase : IGetBooksByGenreUseCase
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

        public async Task<GetAllBooksResponse> ExecuteAsync(GetBooksByGenreRequest request, CancellationToken cancellationToken)
        {
            var (bookEntities, totalCount) = await _unitOfWork.BooksRepository.GetBooksByGenreAsync(request.genre, request.pageNumber, request.pageSize, cancellationToken);

            var books = _mapper.Map<List<Book>>(bookEntities);

            return new GetAllBooksResponse(books, totalCount);
        }
    }
}
