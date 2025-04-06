using Lib.Core.Abstractions;
using Lib.Core.Exceptions;

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

            var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException($"Book with id {id} is not found");
            }

            var guid = await _unitOfWork.BooksRepository.DeleteBookAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return guid;
        }
    }
}
