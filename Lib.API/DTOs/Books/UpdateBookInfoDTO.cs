namespace Lib.API.DTOs.Books
{
    public record UpdateBookInfoDTO
    (
        Guid Id,
        string ISBN,
        string Name,
        string Description
    );
}
