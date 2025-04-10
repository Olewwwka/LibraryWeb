namespace Lib.API.DTOs.Authors
{
    public record AddAuthorDTO
    (
        string Name,
        string Surname,
        string Country,
        DateTime Birthday
    );
}
