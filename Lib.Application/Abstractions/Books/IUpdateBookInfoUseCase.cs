using Lib.Application.Contracts.Requests;
using Lib.Application.Models;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Books
{
    public interface IUpdateBookInfoUseCase : IUseCase<UpdateBookInfoRequest, Book> { }
}
