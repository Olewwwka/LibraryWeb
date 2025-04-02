using System.ComponentModel.DataAnnotations;
using Lib.Core.Enums;

namespace Lib.API.Contracts
{
    public record BookRequest
    (
        [Required] string ISBN,
        [Required] string Name,
        [Required] Genre Genre ,
        [Required] string Description,
        [Required] Guid AuthorId
    );
    
}
