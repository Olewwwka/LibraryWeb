using Lib.Application.Abstractions.Users;
using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Abstractions.Services;
using Lib.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Lib.Application.UseCases.Auth
{
    public class RefreshTokenUseCase : IRefreshTokenUseCase
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public RefreshTokenUseCase(
            ITokenService tokenService,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<RefreshTokenResponse> ExecuteAsync(
            RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.OldRefreshToken) || string.IsNullOrEmpty(request.OldAccessToken))
            {
                throw new UnauthorizedAccessException("Invalid tokens");
            }

            var principal = _tokenService.GetPrincipalFromExpiredToken(request.OldAccessToken);
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid token claims");
            }

            var cacheKey = $"refresh-token:{userId}";
            var storedRefreshToken = await _cacheService.GetAsync(cacheKey);

            if (storedRefreshToken != request.OldRefreshToken)
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

            await _tokenService.StoreRefreshTokenAsync(userId, newRefreshToken);

            return new RefreshTokenResponse(newAccessToken, newRefreshToken);
        }
    }
}