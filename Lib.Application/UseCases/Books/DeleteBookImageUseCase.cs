using AutoMapper;
using Lib.Application.Abstractions.Books;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions;
using Lib.Core.Abstractions.Repositories;

namespace Lib.Application.UseCases.Books
{
    public class DeleteBookImageUseCase : IDeleteBookImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public DeleteBookImageUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<string> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {

            var book = await _unitOfWork.BooksRepository.GetBookByIdAsync(id, cancellationToken);

            if (book == null)
            {
                throw new NotFoundException($"Book with id {id} is not found");
            }

            if (book.ImagePath == "default_image.jpg")
            {
                throw new ArgumentNullException("Book dont have uploaded image!");
            }

            _fileService.Delete(book.ImagePath);

            var result = await _unitOfWork.BooksRepository.UpdateBookImageAsync(id, "default_image.jpg", cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
