using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;

namespace Lib.Application.UseCases.Books
{
    public class GetBookByIdUseCase
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

            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
