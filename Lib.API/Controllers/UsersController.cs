using Lib.API.Contracts;
using Lib.Application.UseCases.Auth;
using Lib.Application.UseCases.Users;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public BorrowBookUseCase _borrowBookUseCase;
        public ReturnBookUseCase _returnBookUseCase;
        public GetUsersBorrowedBooksUseCase _getUsersBorrowedBooksUseCase;
        public RefreshTokenUseCase _refreshTokenUseCase;

        public UsersController(
            BorrowBookUseCase borrowBookUseCase,
            ReturnBookUseCase returnBookUSeCase,
            GetUsersBorrowedBooksUseCase getUsersBorrowedBooksUseCase,
            RefreshTokenUseCase refreshTokenUseCase)
        {
            _borrowBookUseCase = borrowBookUseCase;
            _returnBookUseCase = returnBookUSeCase;
            _getUsersBorrowedBooksUseCase = getUsersBorrowedBooksUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost("book/borrow")]
        [Authorize]
        public async Task<IResult> BorrowBook(BorrowBookRequest request, CancellationToken cancellationToken)
        {
            var book = await _borrowBookUseCase.ExecuteAsync(request.UserId, request.BookId, request.BorrowTime, request.ReturnTime, cancellationToken);

            return Results.Ok(book);
        }

        [HttpPost("book/return/{bookId}")]
        [Authorize]
        public async Task<IResult> ReturnBook(Guid bookId, CancellationToken cancellationToken)
        {
            var book = await _returnBookUseCase.ExecuteAsync(bookId, cancellationToken);

            return Results.Ok(book);
        }

        [HttpGet("books")]
        [Authorize]
        public async Task<IResult> GetUserBorrowedBooks(Guid id, CancellationToken cancellationToken)
        {
            var books = await _getUsersBorrowedBooksUseCase.ExecuteAsync(id, cancellationToken);

            return Results.Ok(books);
        }

        [HttpPost("refresh-token")]
        public async Task<IResult> RefreshToken(CancellationToken cancellationToken)
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var accessToken = Request.Cookies["jwtToken"];

                var (newAccessToken, newRefreshToken, user) =
                    await _refreshTokenUseCase.ExecuteAsync(accessToken, refreshToken, cancellationToken);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(3)
                };

                Response.Cookies.Append("jwtToken", newAccessToken, cookieOptions);
                Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

                return Results.Ok(new
                {
                    AccessToken = newAccessToken,
                    User = user
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Results.Unauthorized();
            }
        }
    }
}