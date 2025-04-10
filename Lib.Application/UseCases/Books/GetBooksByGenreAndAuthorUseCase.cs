using AutoMapper;
using Lib.Application.Models;
using Lib.Core.Enums;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Responses;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Books
{
    public class GetBooksByGenreAndAuthorUseCase : IGetBooksByGenreAndAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBooksByGenreAndAuthorUseCase(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetAllBooksResponse> ExecuteAsync(GetBooksByGenreAndAuthorRequest request, CancellationToken cancellationToken)
        {
            var (bookEntities, totalCount) = await _unitOfWork.BooksRepository.GetBooksByGenreAndAuthorAsync(request.genre, request.authorId, request.pageNumber, request.pageSize, cancellationToken);

            var books = _mapper.Map<List<Book>>(bookEntities);

            return new GetAllBooksResponse(books, totalCount);
        }
    }
} 