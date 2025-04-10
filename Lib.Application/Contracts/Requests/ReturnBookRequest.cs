namespace Lib.Application.Contracts.Requests
{
    public record ReturnBookRequest
    (
        Guid UserId,
        Guid BookId
    );
}
