using AutoMapper;
using Lib.Application.Models;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Exceptions;
using Lib.Application.Contracts.Requests;
using Lib.Application.Abstractions.Users;

namespace Lib.Application.UseCases.Users
{
    public class BorrowBookUseCase : IBorrowBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BorrowBookUseCase(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> ExecuteAsync(BorrowBookRequest request, CancellationToken cancellationToken)
        {

            var bookEntity = await _unitOfWork.BooksRepository.GetBookByIdAsync(request.bookId, cancellationToken);

            if(bookEntity is null)
            {
                throw new NotFoundException($"Book with id {request.bookId} not found");
            }

            var userEntity = await _unitOfWork.UsersRepository.GetUserByIdAsync(request.userId, cancellationToken);

            if (userEntity is null)
            {
                throw new NotFoundException($"User with id {request.userId} not found");
            }

            bookEntity.BorrowTime = request.borrowTime;
            bookEntity.ReturnTime = request.returnTime;
            bookEntity.UserId = request.userId;


            _unitOfWork.BooksRepository.UpdateBook(bookEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            

            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
