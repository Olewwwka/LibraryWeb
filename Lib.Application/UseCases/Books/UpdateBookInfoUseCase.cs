using Lib.Core.Abstractions;
using Lib.Core.Enums;

namespace Lib.Application.UseCases.Books
{
    public class UpdateBookInfoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookInfoUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(Guid id, string isbn, string name, Genre genre, string description, CancellationToken cancellationToken)
        {
            var guid = await _unitOfWork.BooksRepository.UpdateBookAsync(id, isbn, name, genre, description, cancellationToken);

            return guid;
        }
    }
}
