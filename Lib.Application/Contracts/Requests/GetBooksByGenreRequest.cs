using Lib.Core.Enums;

namespace Lib.Application.Contracts.Requests
{
    public record GetBooksByGenreRequest
    (
        Genre genre, 
        int pageNumber, 
        int pageSize
    );
}
