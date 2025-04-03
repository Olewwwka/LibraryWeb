using Lib.API.Contracts;
using Lib.Application.UseCases.Users;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public BorrowBookUseCase _borrowBookUseCase;
        public ReturnBookUseCase _returnBookUseCase;
        public GetUsersBorrowedBooksUseCase _getUsersBorrowedBooksUseCase;
        public UsersController(BorrowBookUseCase borrowBookUseCase,
            ReturnBookUseCase returnBookUSeCase,
            GetUsersBorrowedBooksUseCase getUsersBorrowedBooksUseCase)
        {
            _borrowBookUseCase = borrowBookUseCase;
            _returnBookUseCase = returnBookUSeCase;
            _getUsersBorrowedBooksUseCase = getUsersBorrowedBooksUseCase;
        }

        [HttpPost("book/borrow")]
        public async Task<IResult> BorrowBook(BorrowBookRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _borrowBookUseCase.ExecuteAsync(request.UserId, request.BookId, request.BorrowTime, request.ReturnTime, cancellationToken);
                return Results.Ok(book);
            }
            catch(BookAlreadyBorrowedException)
            {
                throw new BookAlreadyBorrowedException("Book is already borrowed");
            }
            catch(NotFoundException)
            {
                throw new NotFoundException($"Book with ID {request.BookId} not found");
            }

        }
        [HttpPost("book/return/{bookId}")]
        public async Task<IResult> ReturnBook(Guid bookId, CancellationToken cancellationToken)
        {
            var book = await _returnBookUseCase.ExecuteAsync(bookId, cancellationToken);
            return Results.Ok(book);
        }
        [HttpGet("books")]
        public async Task<IResult> GetUserBorrowedBooks(Guid id, CancellationToken cancellationToken)
        {
            var books = await _getUsersBorrowedBooksUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(books);
        }

    }
}