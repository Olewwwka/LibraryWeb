using Lib.API.Contracts;
using Lib.Application.Services;
using Lib.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {

        private readonly IBooksService _booksService;
        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        public async Task<IResult> GetBooks(CancellationToken cancellationToken)
        {
            var books = await _booksService.GetAllBooks(cancellationToken);

            return Results.Ok(books);
        }

        [HttpPost]
        public async Task<IResult> AddBook(AddBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _booksService.AddBook(request.ISBN, request.Name, request.Genre, request.Description, request.AuthorId, cancellationToken);

            return Results.Ok(book);
        
        }
        [HttpGet("{id}")]
        public async Task<IResult> GetBookById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _booksService.GetBookById(id, cancellationToken);

            return Results.Ok(book);
        }

        [HttpGet("isbn/{isbn}")]
        public async Task<IResult> GetBookByISBN(string isbn, CancellationToken cancellationToken)
        {
            var book = await _booksService.GetBookByISBN(isbn, cancellationToken);

            return Results.Ok(book);
        }

        [HttpPatch("{id}")]
        public async Task<IResult> UpdateBook(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _booksService.UpdateBookInfo(id, request.ISBN, request.Name, request.Genre, request.Description, cancellationToken);
            return Results.Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteBook(Guid id, CancellationToken cancellationToken)
        {
            var bookId = await _booksService.DeleteBook(id, cancellationToken);

            return Results.Ok(bookId);
        }
    }
}
