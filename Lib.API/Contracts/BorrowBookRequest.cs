namespace Lib.API.Contracts
{
    public record BorrowBookRequest
    (
        Guid UserId,
        Guid BookId,
        DateTime BorrowTime,
        DateTime ReturnTime
    );
}
