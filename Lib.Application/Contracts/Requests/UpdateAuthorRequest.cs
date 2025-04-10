namespace Lib.Application.Contracts.Requests
{
    public record UpdateAuthorRequest
    (
        Guid Id,
        string Name, 
        string Surname,
        string Country,
        DateTime Birthday
    );
}
