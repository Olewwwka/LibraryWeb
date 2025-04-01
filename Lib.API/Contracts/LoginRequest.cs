using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record LoginRequest
    (
       [Required] string Email,
       [Required] string Password
     );
}
