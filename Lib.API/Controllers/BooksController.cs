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
        public async Task<IResult> AddBook(BookRequest request, CancellationToken cancellationToken)
        {
            var book = await _booksService.AddBook(request.ISBN, request.Name, request.Genre, request.Description, request.AuthorId, cancellationToken);

            return Results.Ok(book);
        }
    }
}
