using Lib.Application.Contracts.Requests;
using Lib.Application.Models;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Users
{
    public interface IReturnBookUseCase : IUseCase<ReturnBookRequest, Book> { }
}
