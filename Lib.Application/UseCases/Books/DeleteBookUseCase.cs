using Lib.Core.Abstractions.Repositories;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions;
using Lib.Application.Abstractions.Books;

namespace Lib.Application.UseCases.Books
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        public DeleteBookUseCase(
            IUnitOfWork unitOfWork,
            IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Guid> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {

            var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException($"Book with id {id} is not found");
            }

            if (book.ImagePath != "default_image.jpg")
            {
                _fileService.Delete(book.ImagePath);
            }

            var guid = await _unitOfWork.BooksRepository.RemoveBookAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return guid;
        }
    }
}
