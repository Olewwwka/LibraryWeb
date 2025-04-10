namespace Lib.Application.Contracts.Requests
{
    public record BorrowBookRequest
    (
        Guid userId,
        Guid bookId,
        DateTime borrowTime,
        DateTime returnTime
    );
}
