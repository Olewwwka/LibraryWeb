using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record RegisterRequest
    (
        string Name,
        string Email,
        string Password
    );
}
