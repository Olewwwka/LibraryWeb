using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record AuthorRequest(
        [Required] string Name,
        [Required] string Surname,
        [Required] string Country,
        [Required] DateTime Birthday
        );
}
