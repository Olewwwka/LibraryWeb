using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;
using Lib.Core.Exceptions;

namespace Lib.Application.UseCases.Books
{
    public class GetBookByISBNUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetBookByISBNUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> ExecuteAsync(string isbn, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.BooksRepository.GetBookByISBNAsync(isbn, cancellationToken);

            if (bookEntity == null)
            {
                throw new InvalidISBNException($"Book with ISBN {isbn} is not found");
            }

            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
