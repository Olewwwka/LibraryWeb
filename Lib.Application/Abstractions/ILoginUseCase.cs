using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions
{
    public interface ILoginUseCase : IUseCase<LoginRequest, LoginResponse> { }
}
