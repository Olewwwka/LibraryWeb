using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Lib.API.Contracts.RegisterRequest;
using LoginRequest = Lib.API.Contracts.LoginRequest;
using Lib.Application.UseCases.Auth;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        public LoginUseCase _loginUseCase;
        public RegisterUseCase _registerUseCase;

        public AuthController(LoginUseCase loginUseCase,
            RegisterUseCase registerUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
        }

        [HttpPost("register")]
        public async Task<IResult> Register(RegisterRequest request, CancellationToken cancellationToken)
        { 
            var user = await _registerUseCase.ExecuteAsync(request.Name, request.Email, request.Password, cancellationToken);

            return Results.Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.Role
            });
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var (user, accessToken, refreshToken) = await _loginUseCase.ExecuteAsync(request.Email, request.Password, cancellationToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(3)
            };

            var Responce = HttpContext.Response;
            Responce.Cookies.Append("jwtToken", accessToken, cookieOptions);
            Responce.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            return Results.Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.Role,
                accessToken,
                refreshToken,
            });
        }
    }
}
