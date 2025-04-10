namespace Lib.Application.Contracts.Requests
{
    public record UpdateBookInfoRequest
    (
       Guid Id,
       string ISBN,
       string Name,
       string Description
    );
}
