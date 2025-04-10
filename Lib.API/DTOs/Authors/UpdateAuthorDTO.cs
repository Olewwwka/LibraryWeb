namespace Lib.API.DTOs.Authors
{
    public record UpdateAuthorDTO
    (
        Guid Id,
        string Name,
        string Surname,
        string Country,
        DateTime Birthday
    );
}
