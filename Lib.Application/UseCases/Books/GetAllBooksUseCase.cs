using AutoMapper;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Application.Models;
using Lib.Core.Abstractions.Repositories;

namespace Lib.Application.UseCases.Books
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
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

        public async Task<GetAllBooksResponse> ExecuteAsync(GetAllBooksRequest request, CancellationToken cancellationToken)
        {
            var (bookEntities, totalCount) = await _unitOfWork.BooksRepository.GetPaginatedBooksAsync(request.PageNumber, request.PageSize, cancellationToken);
            
            var books = _mapper.Map<List<Book>>( bookEntities);

            return new GetAllBooksResponse (books, totalCount);

        }
    }
}
