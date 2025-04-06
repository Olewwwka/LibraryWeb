using Lib.API.Contracts;
using Lib.Application.UseCases.Books;
using Lib.Core.Enums;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lib.Core.Abstractions;
using Microsoft.AspNetCore.Http;

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
        public GetBooksByGenreUseCase _getBooksByGenreUseCase;
        public GetBooksByGenreAndAuthorUseCase _getBooksByGenreAndAuthorUseCase;
        public UploadBookImageUseCase _uploadBookImageUseCase;
        public DeleteBookImageUseCase _deleteBookImageUseCase;

        public BooksController(
            AddBookUseCase addBookUseCase,
            GetAllBooksUseCase getAllBooksUseCase, 
            GetBookByIdUseCase getBookByIdUseCase, 
            GetBookByISBNUseCase getBookByISBNUseCase, 
            UpdateBookInfoUseCase updateBookInfoUseCase, 
            DeleteBookUseCase deleteBookUseCase,
            GetBooksByGenreUseCase getBooksByGenreUseCase,
            GetBooksByGenreAndAuthorUseCase getBooksByGenreAndAuthorUseCase,
            UploadBookImageUseCase uploadBookImageUseCase,
            DeleteBookImageUseCase deleteBookImageUseCase
            )
        {
            _addBookUseCase = addBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _updateBookInfoUseCase = updateBookInfoUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _getBooksByGenreUseCase = getBooksByGenreUseCase;
            _getBooksByGenreAndAuthorUseCase = getBooksByGenreAndAuthorUseCase;
            _uploadBookImageUseCase = uploadBookImageUseCase;
            _deleteBookImageUseCase = deleteBookImageUseCase;
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

        [HttpGet("genre/{genre}")]
        [Authorize]
        public async Task<IResult> GetBooksByGenre(Genre genre, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var (books, totalCount) = await _getBooksByGenreUseCase.ExecuteAsync(genre, pageNumber, pageSize, cancellationToken);

            return Results.Ok(new
            {
                Books = books,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }

        [HttpGet("genre/{genre}/author/{authorId}")]
        [Authorize]
        public async Task<IResult> GetBooksByGenreAndAuthor(Genre genre, Guid authorId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var (books, totalCount) = await _getBooksByGenreAndAuthorUseCase.ExecuteAsync(genre, authorId, pageNumber, pageSize, cancellationToken);

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

        [HttpPost("{id}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> UploadBookImage(Guid id, IFormFile imageFile, CancellationToken cancellationToken)
        {
            var imagePath = await _uploadBookImageUseCase.ExecuteAsync(id, imageFile, cancellationToken);
            return Results.Ok(new { imagePath });
        }

        [HttpDelete("{id}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> DeleteBookImage(Guid id, string filePath, CancellationToken cancellationToken)
        {
            var path = await _deleteBookImageUseCase.ExecuteAsync(id, filePath, cancellationToken);
            return Results.Ok(path);
        }
    }
}
