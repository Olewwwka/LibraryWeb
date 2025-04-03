using Lib.Core.Abstractions;

namespace Lib.Application.UseCases.Books
{
    public class DeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var guid = await _unitOfWork.BooksRepository.DeleteBookAsync(id, cancellationToken);
            return guid;
        }
    }
}
