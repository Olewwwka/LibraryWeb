using Lib.Core.Abstractions;
using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;

namespace Lib.Application.Abstractions.Books
{
    public interface IGetBooksByGenreAndAuthorUseCase : IUseCase<GetBooksByGenreAndAuthorRequest, GetAllBooksResponse> { }
}
