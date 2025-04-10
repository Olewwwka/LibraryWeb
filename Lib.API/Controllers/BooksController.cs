using AutoMapper;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Requests;
using Lib.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        public IAddBookUseCase _addBookUseCase;
        public IGetAllBooksUseCase _getAllBooksUseCase;
        public IGetBookByIdUseCase _getBookByIdUseCase;
        public IGetBookByISBNUseCase _getBookByISBNUseCase;
        public IUpdateBookInfoUseCase _updateBookInfoUseCase;
        public IDeleteBookUseCase _deleteBookUseCase;
        public IGetBooksByGenreUseCase _getBooksByGenreUseCase;
        public IGetBooksByGenreAndAuthorUseCase _getBooksByGenreAndAuthorUseCase;
        public IUploadBookImageUseCase _uploadBookImageUseCase;
        public IDeleteBookImageUseCase _deleteBookImageUseCase;
        private readonly IMapper _mapper;

        public BooksController(
            IAddBookUseCase addBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase, 
            IGetBookByIdUseCase getBookByIdUseCase, 
            IGetBookByISBNUseCase getBookByISBNUseCase, 
            IUpdateBookInfoUseCase updateBookInfoUseCase, 
            IDeleteBookUseCase deleteBookUseCase,
            IGetBooksByGenreUseCase getBooksByGenreUseCase,
            IGetBooksByGenreAndAuthorUseCase getBooksByGenreAndAuthorUseCase,
            IUploadBookImageUseCase uploadBookImageUseCase,
            IDeleteBookImageUseCase deleteBookImageUseCase,
            IMapper mapper
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
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IResult> GetBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var request = new GetAllBooksRequest(pageNumber, pageSize);

            var response = await _getAllBooksUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(new
            {
                response.Books,
                TotalCount = response.TotalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(response.TotalPages / (double)pageSize)
            });
        }

        [HttpGet("genre/{genre}")]
        [Authorize]
        public async Task<IResult> GetBooksByGenre(Genre genre, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {

            var request = new GetBooksByGenreRequest(genre, pageNumber, pageSize);
            var response = await _getBooksByGenreUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(new
            {
                response.Books,
                TotalCount = response.TotalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(response.TotalPages / (double)pageSize)
            });
        }

        [HttpGet("genre/{genre}/author/{authorId}")]
        [Authorize]
        public async Task<IResult> GetBooksByGenreAndAuthor(Genre genre, Guid authorId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {

            var request = new GetBooksByGenreAndAuthorRequest(genre, authorId, pageNumber, pageSize);

            var response = await _getBooksByGenreAndAuthorUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(new
            {
                response.Books,
                TotalCount = response.TotalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(response.TotalPages / (double)pageSize)
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> AddBook([FromBody]AddBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _addBookUseCase.ExecuteAsync(request, cancellationToken);

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
        public async Task<IResult> UpdateBook([FromBody]UpdateBookInfoRequest request, CancellationToken cancellationToken)
        {
            var book = await _updateBookInfoUseCase.ExecuteAsync(request, cancellationToken);

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
            var request = new UploadBookImageRequest(id, imageFile);

            var imagePath = await _uploadBookImageUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(new { imagePath });
        }

        [HttpDelete("{id}/image")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> DeleteBookImage(Guid id, CancellationToken cancellationToken)
        {
            var path = await _deleteBookImageUseCase.ExecuteAsync(id, cancellationToken);
            return Results.Ok(path);
        }
    }
}
