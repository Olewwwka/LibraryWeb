using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.UseCases.Auth
{
    public class LoginUseCase
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        public LoginUseCase(ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            IMapper mapper)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        public async Task<(User, string accessToken, string RefreshToken)> ExecuteAsync(string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email, cancellationToken);
            if (existingUser == null) throw new Exception();  //========================= 

            if (!_passwordHasher.Verify(password, existingUser.PasswordHash)) throw new Exception();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new(ClaimTypes.Email, existingUser.Email),
                new(ClaimTypes.Role, existingUser.Role)
            };

            var jwtToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.StoreRefreshTokenAsync(existingUser.Id, refreshToken);

            var user = _mapper.Map<User>(existingUser);

            return (user, jwtToken, refreshToken);
        }
    }
}
