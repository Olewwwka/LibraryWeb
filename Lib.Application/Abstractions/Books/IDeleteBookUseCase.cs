using Lib.Core.Abstractions;

namespace Lib.Application.Abstractions.Books
{
    public interface IDeleteBookUseCase : IUseCase<Guid, Guid> { }
}
