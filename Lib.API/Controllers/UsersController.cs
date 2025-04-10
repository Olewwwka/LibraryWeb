using AutoMapper;
using Lib.Application.Abstractions.Users;
using Lib.Application.Contracts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public IBorrowBookUseCase _borrowBookUseCase;
        public IReturnBookUseCase _returnBookUseCase;
        public IGetUsersBorrowedBooksUseCase _getUsersBorrowedBooksUseCase;
        public IRefreshTokenUseCase _refreshTokenUseCase;
        private readonly IMapper _mapper;

        public UsersController(
            IBorrowBookUseCase borrowBookUseCase,
            IReturnBookUseCase returnBookUSeCase,
            IGetUsersBorrowedBooksUseCase getUsersBorrowedBooksUseCase,
            IRefreshTokenUseCase refreshTokenUseCase,
            IMapper mapper)
        {
            _borrowBookUseCase = borrowBookUseCase;
            _returnBookUseCase = returnBookUSeCase;
            _getUsersBorrowedBooksUseCase = getUsersBorrowedBooksUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
            _mapper = mapper;
        }

        [HttpPost("book/borrow")]
        [Authorize]
        public async Task<IResult> BorrowBook([FromBody]BorrowBookRequest request, CancellationToken cancellationToken)
        {
            
            var book = await _borrowBookUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(book);
        }

        [HttpPost("book/return/{userId}/{bookId}")]
        [Authorize]
        public async Task<IResult> ReturnBook(Guid userId, Guid bookId, CancellationToken cancellationToken)
        {
            var request = new ReturnBookRequest(userId, bookId);

            var book = await _returnBookUseCase.ExecuteAsync(request, cancellationToken);

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
                var refreshToken = Request.Cookies["refreshToken"];
                var accessToken = Request.Cookies["jwtToken"];

                var request = new RefreshTokenRequest(accessToken,refreshToken);

                var (newAccessToken, newRefreshToken) =
                    await _refreshTokenUseCase.ExecuteAsync(request, cancellationToken);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(3)
                };

                Response.Cookies.Append("jwtToken", newAccessToken, cookieOptions);
                Response.Cookies.Append("refreshToken", newRefreshToken, cookieOptions);

                return Results.Ok(new
                {
                    AccessToken = newAccessToken,
                });
            }
        }
    }
