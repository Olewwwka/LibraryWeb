using AutoMapper;
using Lib.Application.Models;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Abstractions.Books;

namespace Lib.Application.UseCases.Books
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBookByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.BooksRepository.GetBookByIdAsync(id, cancellationToken);

            if (bookEntity == null)
            {
                throw new NotFoundException($"Book with id {id} is not found");
            }
            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
