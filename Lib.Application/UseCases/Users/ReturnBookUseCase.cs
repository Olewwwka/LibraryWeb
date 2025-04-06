using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;

namespace Lib.Application.UseCases.Users
{
    public class ReturnBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnBookUseCase(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> ExecuteAsync(Guid bookId, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.UsersRepository.ReturnBookAsync(bookId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
