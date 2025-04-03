using Lib.API.Contracts;
using Lib.Application.UseCases.Authors;
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

            var author = await _addAuthorUseCase.ExecuteAsync(request.Name, request.Surname, request.Birthday, request.Country, cancellationToken);

            return Results.Ok(author);
        }
        [HttpGet]
        public async Task<IResult> GetAuthors(CancellationToken cancellationToken)
        {
            var authors = await _getAllAuthorsUseCase.ExecuteAsync(cancellationToken);

            return Results.Ok(authors);
        }
    }
}
