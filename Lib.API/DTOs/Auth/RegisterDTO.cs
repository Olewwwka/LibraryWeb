namespace Lib.API.DTOs.Auth
{
    public record RegisterDTO
    (
        string Name,
        string Email,
        string Password
    );
}
