using Lib.Core.Abstractions.Repositories;
using Lib.Core.Abstractions.Services;
using Lib.Core.Entities;
using Lib.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Security.Claims;

namespace Lib.Application.UseCases.Auth
{
    public class RefreshTokenUseCase
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConnectionMultiplexer _redis;

        public RefreshTokenUseCase(
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            IConnectionMultiplexer redis)
        {
            _redis = redis;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(string AccessToken, string RefreshToken, UserEntity User)> ExecuteAsync(
            string oldAccessToken,
            string oldRefreshToken,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(oldRefreshToken) || string.IsNullOrEmpty(oldAccessToken))
            {
                throw new UnauthorizedAccessException("Invalid tokens");
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(oldAccessToken);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid token claims");
            }

            var db = _redis.GetDatabase();
            var storedUserId = await db.StringGetAsync(oldRefreshToken);

            if (storedUserId != userId.ToString())
            {
                throw new UnauthorizedAccessException("Refresh token mismatch");
            }

            var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            await db.KeyDeleteAsync(oldRefreshToken);
            await _tokenService.StoreRefreshTokenAsync(userId, newRefreshToken);

            return (newAccessToken, newRefreshToken, user);
        }
    }
}