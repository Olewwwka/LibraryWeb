using Lib.Core.Entities;
using System.Security.Claims;

namespace Lib.Core.Abstractions.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserEntity user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task StoreRefreshTokenAsync(Guid userId, string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
    }
}
