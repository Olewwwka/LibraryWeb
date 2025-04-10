namespace Lib.Application.Contracts.Requests
{
    public record GetAllBooksRequest
    (
        int PageNumber, 
        int PageSize
    );
}
