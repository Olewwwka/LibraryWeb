namespace Lib.Application.Contracts.Requests
{
    public record RegisterRequest
    (
        string Name, 
        string Email,
        string Password
    );
}
