using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record RegisterRequest
    (
        [Required] string Name,
        [Required] string Email,
        [Required] string Password
    );
}
