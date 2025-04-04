using Lib.API.Contracts;
using Lib.Application.UseCases.Books;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        public AddBookUseCase _addBookUseCase;
        public GetAllBooksUseCase _getAllBooksUseCase;
        public GetBookByIdUseCase _getBookByIdUseCase;
        public GetBookByISBNUseCase _getBookByISBNUseCase;
        public UpdateBookInfoUseCase _updateBookInfoUseCase;
        public DeleteBookUseCase _deleteBookUseCase;

        public BooksController(
            AddBookUseCase addBookUseCase,
            GetAllBooksUseCase getAllBooksUseCase, 
            GetBookByIdUseCase getBookByIdUseCase, 
            GetBookByISBNUseCase getBookByISBNUseCase, 
            UpdateBookInfoUseCase updateBookInfoUseCase, 
            DeleteBookUseCase deleteBookUseCase)
        {
            _addBookUseCase = addBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _updateBookInfoUseCase = updateBookInfoUseCase;
            _deleteBookUseCase = deleteBookUseCase;
        }

        [HttpGet]
        [Authorize]
        public async Task<IResult> GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var (books, totalCount) = await _getAllBooksUseCase.ExecuteAsync(pageNumber, pageSize, cancellationToken);

            return Results.Ok(new
            {
                Books = books,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> AddBook(AddBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _addBookUseCase.ExecuteAsync(request.ISBN, request.Name, request.Genre, request.Description, request.AuthorId, cancellationToken);

            return Results.Ok(book);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        [Authorize]
        public async Task<IResult> GetBookByISBN(string isbn, CancellationToken cancellationToken)
        {
            var book = await _getBookByISBNUseCase.ExecuteAsync(isbn, cancellationToken);

            return Results.Ok(book);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> UpdateBook(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _updateBookInfoUseCase.ExecuteAsync(id, request.ISBN, request.Name, request.Genre, request.Description, cancellationToken);

            return Results.Ok(book);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            var bookId = await _deleteBookUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(bookId);
        }

        [HttpPost("{id}/borrow")]
        [Authorize]
        public async Task<IResult> BorrowBook(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok();
        }
    }
}
