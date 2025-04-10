using Lib.Core.Enums;

namespace Lib.Application.Contracts.Requests
{
    public record AddBookRequest
    (
        string ISBN,
        string Name,
        Genre Genre,
        string Description,
        Guid AuthorId
    );
}
