using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Users
{
    public interface IRefreshTokenUseCase : IUseCase<RefreshTokenRequest, RefreshTokenResponse> { }
}
