using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Lib.Application.Contracts.Requests;
using Lib.Application.Abstractions;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        public ILoginUseCase _loginUseCase;
        public IRegisterUseCase _registerUseCase;

        public AuthController(
            IMapper mapper,
            ILoginUseCase loginUseCase,
            IRegisterUseCase registerUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody]RegisterRequest request, CancellationToken cancellationToken)
        { 
            var response = await _registerUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(response);
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody]LoginRequest request, CancellationToken cancellationToken)
        {
            var response = await _loginUseCase.ExecuteAsync(request, cancellationToken);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddHours(3)
            };

            var Responce = HttpContext.Response;
            Responce.Cookies.Append("jwtToken", response.AccessToken, cookieOptions);
            Responce.Cookies.Append("refreshToken", response.RefreshToken, cookieOptions);

            return Results.Ok(new
            {
                response.Id,
                response.Email,
                response.Name,
                response.Role,
            });
        }
    }
}
