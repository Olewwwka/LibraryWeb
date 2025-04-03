using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record AuthorRequest(
        string Name,
        string Surname,
        string Country,
        DateTime Birthday
        );
}
