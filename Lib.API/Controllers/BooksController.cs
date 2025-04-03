using Lib.API.Contracts;
using Lib.Application.UseCases.Books;
using Lib.Core.Exceptions;
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
            if (string.IsNullOrEmpty(request.ISBN) || request.ISBN.Length < 10)
            {
                throw new InvalidISBNException("ISBN must have 10+ characters");
            }

            var book = await _addBookUseCase.ExecuteAsync(request.ISBN, request.Name, request.Genre, request.Description, request.AuthorId, cancellationToken);
            return Results.Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {id} not found");
            }
            return Results.Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IResult> GetBookByISBN(string isbn, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(isbn) || isbn.Length < 10)
            {
                throw new InvalidISBNException("ISBN must have 10+ characters");
            }

            var book = await _getBookByISBNUseCase.ExecuteAsync(isbn, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException($"Book with ISBN {isbn} not found");
            }
            return Results.Ok(book);
        }

        [HttpPatch("{id}")]
        public async Task<IResult> UpdateBook(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.ISBN) && request.ISBN.Length < 10)
            {
                throw new InvalidISBNException("ISBN must have 10+ characters");
            }

            var book = await _updateBookInfoUseCase.ExecuteAsync(id, request.ISBN, request.Name, request.Genre, request.Description, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {id} not found");
            }
            return Results.Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            var bookId = await _deleteBookUseCase.ExecuteAsync(id, cancellationToken);
            if (bookId == Guid.Empty)
            {
                throw new NotFoundException($"Book with ID {id} not found");
            }
            return Results.Ok(bookId);
        }

        [HttpPost("{id}/borrow")]
        public async Task<IResult> BorrowBook(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException($"Book with ID {id} not found");
            }

            if (book.IsBorrowed)
            {
                throw new BookAlreadyBorrowedException($"Book with ID {id} is already borrowed");
            }

            // Здесь будет логика выдачи книги
            return Results.Ok();
        }
    }
}
