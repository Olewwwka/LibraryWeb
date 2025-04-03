using Lib.API.Contracts;
using Lib.Application.UseCases.Authors;
using Lib.Core.Exceptions;
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
        public async Task<IResult> AddAuthor(AuthorRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Surname))
            {
                throw new ValidationException("Author name and surname cannot be empty");
            }

            if (request.Birthday > DateTime.UtcNow)
            {
                throw new ValidationException("Birthday cannot be in the future");
            }

            var author = await _addAuthorUseCase.ExecuteAsync(request.Name, request.Surname, request.Birthday, request.Country, cancellationToken);
            return Results.Ok(author);
        }

        [HttpGet]
        public async Task<IResult> GetAuthors(CancellationToken cancellationToken)
        {
            var authors = await _getAllAuthorsUseCase.ExecuteAsync(cancellationToken);
            return Results.Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IResult> GetAuthorById(Guid id, CancellationToken cancellationToken)
        {
            var author = await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken);
            if (author == null)
            {
                throw new NotFoundException($"Author with ID {id} not found");
            }
            return Results.Ok(author);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
        {
            var author = await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken);
            if (author == null)
            {
                throw new NotFoundException($"Author with ID {id} not found");
            }

            var books = await _getAuthorBooksUseCase.ExecuteAsync(id, cancellationToken);
            if (books?.Any() == true)
            {
                throw new ConflictException($"Cannot delete author with ID {id} because they have books");
            }

            await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);
            return Results.Ok();
        }
    }
}
