namespace Lib.Application.Contracts.Requests
{
    public record AddAuthorRequest
    (
        string Name,
        string Surname,
        string Country,
        DateTime Birthday
    );
}
