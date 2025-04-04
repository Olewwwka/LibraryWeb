namespace Lib.API.Contracts
{
    public record UpdateAuthorRequest(
       string Name,
       string Surname,
       string Country,
       DateTime Birthday
       );
}
