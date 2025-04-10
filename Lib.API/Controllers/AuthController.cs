using Microsoft.AspNetCore.Mvc;
using Lib.Application.UseCases.Auth;
using AutoMapper;
using Lib.Application.Contracts.Requests;
using Lib.API.DTOs.Auth;
using Lib.Application.Abstractions;

namespace Lib.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        public ILoginUseCase _loginUseCase;
        public IRegisterUseCase _registerUseCase;
        private readonly IMapper _mapper;

        public AuthController(
            IMapper mapper,
            ILoginUseCase loginUseCase,
            IRegisterUseCase registerUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody]RegisterDTO registerDTO, CancellationToken cancellationToken)
        { 

            var request = _mapper.Map<RegisterRequest>(registerDTO);
            var response = await _registerUseCase.ExecuteAsync(request, cancellationToken);

            return Results.Ok(response);
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody]LoginDTO loginDTO, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<LoginRequest>(loginDTO);

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
