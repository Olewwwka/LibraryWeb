
using Lib.API.Contracts;
using Lib.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("book/borrow")]
        public async Task<IResult> BorrowBook(BorrowBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _usersService.BorrowBook(request.UserId, request.BookId, request.BorrowTime, request.ReturnTime, cancellationToken);
            return Results.Ok(book);
        }
        [HttpPost("book/return/{bookId}")]
        public async Task<IResult> ReturnBook(Guid bookId, CancellationToken cancellationToken)
        {
            var book = await _usersService.ReturnBook(bookId, cancellationToken);
            return Results.Ok(book);
        }

    }
}