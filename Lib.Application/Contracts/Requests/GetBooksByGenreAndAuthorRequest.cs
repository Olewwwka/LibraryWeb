using Lib.Core.Enums;

namespace Lib.Application.Contracts.Requests
{
    public record GetBooksByGenreAndAuthorRequest
    (
            Genre genre,
            Guid authorId,
            int pageNumber,
            int pageSize
    );
}
