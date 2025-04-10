using Lib.Application.Contracts.Requests;
using Lib.Application.Models;
using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Books
{
    public interface IAddBookUseCase : IUseCase<AddBookRequest, Book> { }
}
