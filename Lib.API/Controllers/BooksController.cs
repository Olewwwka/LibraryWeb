using Lib.API.Contracts;
using Lib.Application.UseCases.Books;
using Lib.Core.Abstractions;
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
        public BooksController(AddBookUseCase addBookUseCase,
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
        public async Task<IResult> GetBooks(CancellationToken cancellationToken)
        {
            var books = await _getAllBooksUseCase.ExecuteAsync(cancellationToken);

            return Results.Ok(books);
        }

        [HttpPost]
        public async Task<IResult> AddBook(AddBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _addBookUseCase.ExecuteAsync(request.ISBN, request.Name, request.Genre, request.Description, request.AuthorId, cancellationToken);

            return Results.Ok(book);
        
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IResult> GetBookByISBN(string isbn, CancellationToken cancellationToken)
        {
            var book = await _getBookByISBNUseCase.ExecuteAsync(isbn, cancellationToken);

            return Results.Ok(book);
        }

        [HttpPatch("{id}")]
        public async Task<IResult> UpdateBook(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _updateBookInfoUseCase.ExecuteAsync(id, request.ISBN, request.Name, request.Genre, request.Description, cancellationToken);
            return Results.Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            var bookId = await _deleteBookUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(bookId);
        }
    }
}
