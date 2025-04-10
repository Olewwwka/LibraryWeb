using AutoMapper;
using Lib.Application.Models;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Abstractions.Authors;
using Lib.Application.Contracts.Responses;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Authors
{
    public class GetAuthorBooksUseCase : IGetAllAuthorsBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllBooksResponse> ExecuteAsync(GetAllAuthorBooksRequest request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(request.id, cancellationToken);

            if (author == null)
            {
                throw new NotFoundException($"Author with id {request.id} not found");
            }

            var (bookEntities, totalCount) = await _unitOfWork.AuthorsRepository.GetBooksByAuthorAsync(request.id, request.pageNumber, request.pageSize, cancellationToken);

            var books = _mapper.Map<List<Book>>(bookEntities);

            return new GetAllBooksResponse(books, totalCount);
        }
    }
}
