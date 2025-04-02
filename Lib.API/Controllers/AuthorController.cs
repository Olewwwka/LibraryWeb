using Lib.API.Contracts;
using Lib.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;
        public AuthorController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        [HttpPost]
        public async Task<IResult> AddAuthor(AuthorRequest request, CancellationToken cancellationToken)
        {
            var author = await _authorsService.AddAuthor(request.Name, request.Surname, request.Birthday, request.Country, cancellationToken);

            return Results.Ok(author);
        }
    }
}
