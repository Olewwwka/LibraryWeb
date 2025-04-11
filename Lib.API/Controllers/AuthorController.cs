using AutoMapper;
using Lib.Application.Abstractions.Authors;
using Lib.Application.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        public IAddAuthorUseCase _addAuthorUseCase;
        public IGetAuthorByIdUseCase _getAuthorByIdUseCase;
        public IGetAllAuthorsUseCase _getAllAuthorsUseCase;
        public IUpdateAuthorUseCase _updateAuthorInfoUseCase;
        public IDeleteAuthorUseCase _deleteAuthorUseCase;
        public IGetAllAuthorsBooksUseCase _getAuthorBooksUseCase;

        public AuthorController(
            IAddAuthorUseCase addAuthorUseCase,
            IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IGetAllAuthorsUseCase getAllAuthorsUseCase,
            IUpdateAuthorUseCase updateAuthorInfoUseCase,
            IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAllAuthorsBooksUseCase getAuthorBooksUseCase
            )
        {
            _addAuthorUseCase = addAuthorUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _updateAuthorInfoUseCase = updateAuthorInfoUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorBooksUseCase = getAuthorBooksUseCase;
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IResult> AddAuthor([FromBody]AddAuthorRequest request, CancellationToken cancellationToken)
        {
            var author = await _addAuthorUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(author);
        }

        [HttpGet]
        public async Task<IResult> GetAllAuthors(CancellationToken cancellationToken)
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
        {
            await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok();
        }

        [HttpPatch("update")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IResult> UpdateAuthor(UpdateAuthorRequest request, CancellationToken cancellationToken)
        {
            var response = await _updateAuthorInfoUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(response);
        }

        [HttpGet("{id}/books")]
        [Authorize]
        public async Task<IResult> GetAuthorBooks(Guid id, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
        {

            var request = new GetAllAuthorBooksRequest(id, pageNumber, pageSize);
            var response = await _getAuthorBooksUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(new
            {
                response.Books,
                TotalCount = response.TotalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(response.TotalPages / (double)pageSize)
            });
        }
    }
}
