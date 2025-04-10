using Lib.Core.Enums;

namespace Lib.API.DTOs.Books
{
    public record AddBookDTO
     (
         string ISBN,
         string Name,
         Genre Genre,
         string Description,
         Guid AuthorId
     );
}
