namespace Lib.Application.Contracts.Requests
{
    public record GetAllAuthorBooksRequest
    (
        Guid id, 
        int pageNumber, 
        int pageSize
    );
}
