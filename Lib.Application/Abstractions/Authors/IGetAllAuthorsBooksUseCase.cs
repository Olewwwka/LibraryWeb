using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Authors
{
    public interface IGetAllAuthorsBooksUseCase : IUseCase<GetAllAuthorBooksRequest, GetAllBooksResponse> { }
}
