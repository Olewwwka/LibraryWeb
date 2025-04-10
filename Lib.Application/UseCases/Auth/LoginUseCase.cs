using AutoMapper;
using Lib.Application.Models;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Abstractions.Services;
using Lib.Application.Abstractions;
using Lib.Application.Contracts.Responses;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Auth
{
    public class LoginUseCase : ILoginUseCase
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

        public async Task<LoginResponse> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.UsersRepository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (existingUser == null)
            {
                throw new UserNotFoundException($"User with id {request.Email} not found");
            } 

            if (!_passwordHasher.Verify(request.Password, existingUser.PasswordHash))
            {
                throw new InvalidCredentialsException("Invalid password");
            }

            var jwtToken = _tokenService.GenerateAccessToken(existingUser);
            var refreshToken = _tokenService.GenerateRefreshToken();

            await _tokenService.StoreRefreshTokenAsync(existingUser.Id, refreshToken);

            var user = _mapper.Map<User>(existingUser);

            return new LoginResponse(
                    user.Id,
                    user.Name,
                    user.Name,
                    user.Role,
                    jwtToken,
                    refreshToken
                );
        }
    }
}
