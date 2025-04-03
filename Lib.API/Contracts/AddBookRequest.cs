using System.ComponentModel.DataAnnotations;
using Lib.Core.Enums;

namespace Lib.API.Contracts
{
    public record AddBookRequest
    (
        string ISBN,
        string Name,
        Genre Genre ,
        string Description,
        Guid AuthorId
    );
    
}
