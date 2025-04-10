using AutoMapper;
using Lib.API.DTOs.Authors;
using Lib.Application.Abstractions.Authors;
using Lib.Application.Contracts.Requests;
using Lib.Application.Models;
using Lib.Application.UseCases.Authors;
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
        private readonly IMapper _mapper;

        public AuthorController(
            IAddAuthorUseCase addAuthorUseCase,
            IGetAuthorByIdUseCase getAuthorByIdUseCase,
            IGetAllAuthorsUseCase getAllAuthorsUseCase,
            IUpdateAuthorUseCase updateAuthorInfoUseCase,
            IDeleteAuthorUseCase deleteAuthorUseCase,
            IGetAllAuthorsBooksUseCase getAuthorBooksUseCase,
            IMapper mapper)
        {
            _addAuthorUseCase = addAuthorUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _updateAuthorInfoUseCase = updateAuthorInfoUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _getAuthorBooksUseCase = getAuthorBooksUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> AddAuthor([FromBody]AddAuthorDTO addAuthorDTO, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<AddAuthorRequest>(addAuthorDTO);

            var author = await _addAuthorUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(author);
        }

        [HttpGet]
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<IResult> DeleteAuthor(Guid id, CancellationToken cancellationToken)
        {
            await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok();
        }

        [HttpPatch("up/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> UpdateAuthor([FromBody]UpdateAuthorDTO updateAuthorDTO, CancellationToken cancellationToken)
        {
            var author = new Author( updateAuthorDTO.Name, updateAuthorDTO.Surname, updateAuthorDTO.Birthday, updateAuthorDTO.Country);

            author.Id = updateAuthorDTO.Id;

            var request = _mapper.Map<UpdateAuthorRequest>(author);

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
