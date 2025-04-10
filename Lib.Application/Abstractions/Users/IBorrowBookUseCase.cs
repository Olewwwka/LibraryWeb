using Lib.Application.Contracts.Requests;
using Lib.Application.Models;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Users
{
    public interface IBorrowBookUseCase : IUseCase<BorrowBookRequest, Book> { }
}
