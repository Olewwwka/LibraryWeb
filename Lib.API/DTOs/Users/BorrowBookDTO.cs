namespace Lib.API.DTOs.Users
{
    public record BorrowBookDTO
    (
       Guid userId,
       Guid bookId,
       DateTime borrowTime,
       DateTime returnTime
    );
}
