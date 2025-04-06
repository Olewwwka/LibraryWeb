    using Lib.API.Contracts;
using Lib.Application.UseCases.Authors;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        public AddAuthorUseCase _addAuthorUseCase;
        public GetAuthorByIdUseCase _getAuthorByIdUseCase;
        public GetAllAuthorsUseCase _getAllAuthorsUseCase;
        public UpdateAuthorInfoUseCase _updateAuthorInfoUseCase;
        public DeleteAuthorUseCase _deleteAuthorUseCase;
        public GetAuthorBooksUseCase _getAuthorBooksUseCase;

        public AuthorController(AddAuthorUseCase addAuthorUseCase,
            GetAuthorByIdUseCase getAuthorByIdUseCase,
            GetAllAuthorsUseCase getAllAuthorsUseCase,
            UpdateAuthorInfoUseCase updateAuthorInfoUseCase,
            DeleteAuthorUseCase deleteAuthorUseCase,
            GetAuthorBooksUseCase getAuthorBooksUseCase)
        {
            _addAuthorUseCase = addAuthorUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _updateAuthorInfoUseCase = updateAuthorInfoUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorBooksUseCase = getAuthorBooksUseCase;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> AddAuthor(AuthorRequest request, CancellationToken cancellationToken)
        {
            var author = await _addAuthorUseCase.ExecuteAsync(request.Name, request.Surname, request.Birthday, request.Country, cancellationToken);
            return Results.Ok(author);
        }

        [HttpGet]
        [Authorize]
        public async Task<IResult> GetAuthors(CancellationToken cancellationToken)
        {
            var authors = await _getAllAuthorsUseCase.ExecuteAsync(cancellationToken);

            return Results.Ok(authors);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IResult> GetAuthorById(Guid id, CancellationToken cancellationToken)
        {
            var author = await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(author);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
        {
            await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok();
        }

        [HttpPatch("up/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> UpdateAuthor(Guid id, UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            var guid = await _updateAuthorInfoUseCase.ExecuteAsync(id, request.Name, request.Surname, request.Country, request.Birthday, cancellationToken);

            return Results.Ok(guid);
        }

        [HttpGet("{id}/books")]
        [Authorize]
        public async Task<IResult> GetAuthorBooks(Guid id, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var (books, totalCount) = await _getAuthorBooksUseCase.ExecuteAsync(id, pageNumber, pageSize, cancellationToken);

            return Results.Ok(new
            {
                Books = books,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            });
        }
    }
}
