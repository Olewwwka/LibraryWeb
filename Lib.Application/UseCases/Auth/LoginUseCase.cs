using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Lib.Core.Exceptions;

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

            if (existingUser == null)
            {
                throw new UserNotFoundException($"User with id {email} not found");
            } 

            if (!_passwordHasher.Verify(password, existingUser.PasswordHash))
            {
                throw new InvalidCredentialsException("Invalid password");
            }

            var jwtToken = _tokenService.GenerateAccessToken(existingUser);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.StoreRefreshTokenAsync(existingUser.Id, refreshToken);

            var user = _mapper.Map<User>(existingUser);

            return (user, jwtToken, refreshToken);
        }
    }
}
