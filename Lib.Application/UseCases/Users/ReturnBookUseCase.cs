using AutoMapper;
using Lib.Application.Abstractions.Users;
using Lib.Application.Contracts.Requests;
using Lib.Application.Exceptions;
using Lib.Application.Models;
using Lib.Core.Abstractions.Repositories;

namespace Lib.Application.UseCases.Users
{
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnBookUseCase(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> ExecuteAsync(ReturnBookRequest request, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.BooksRepository.GetBookByIdAsync(request.BookId, cancellationToken);

            if (bookEntity is null)
            {
                throw new NotFoundException($"Book with id {request.BookId} not found");
            }

            var userEntity = await _unitOfWork.UsersRepository.GetUserByIdAsync(request.UserId, cancellationToken);

            if(userEntity is null)
            {
                throw new NotFoundException($"User with id {request.UserId} not found");
            }

            if (bookEntity.UserId is not null && bookEntity.UserId != request.UserId)
            {
                throw new NotFoundException($"User with id {request.UserId} not have book with id {request.BookId}");
            }

            bookEntity.BorrowTime = DateTime.MinValue;
            bookEntity.ReturnTime = DateTime.MinValue;
            bookEntity.UserId = null;

            _unitOfWork.BooksRepository.UpdateBook(bookEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
