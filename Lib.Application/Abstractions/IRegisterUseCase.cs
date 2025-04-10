using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions
{
    public interface IRegisterUseCase : IUseCase<RegisterRequest, RegisterResponse> { }
}
