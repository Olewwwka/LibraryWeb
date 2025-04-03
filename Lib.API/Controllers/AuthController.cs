using Lib.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Lib.API.Contracts.RegisterRequest;
using LoginRequest = Lib.API.Contracts.LoginRequest;
using Lib.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<IResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        { 
            var user = await _usersService.Register(request.Name, request.Email, request.Password, cancellationToken);
            return Results.Ok(user);
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var (user, accessToken, refreshToken) = await _usersService.Login(request.Email, request.Password, cancellationToken);
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(3)
            };
            var Responce = HttpContext.Response;
            Responce.Cookies.Append("jwtToken", accessToken, cookieOptions);
            Responce.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            return Results.Ok(user);
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IResult> GetUsers()
        {
            return Results.Ok();
        }

    }
}
