using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record LoginRequest
    (
       string Email,
       string Password
     );
}
