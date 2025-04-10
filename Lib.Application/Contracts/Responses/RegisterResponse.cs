namespace Lib.Application.Contracts.Responses
{
    public record RegisterResponse
    (
        Guid Id,
        string Name,
        string Email,
        string Role
    );
}
