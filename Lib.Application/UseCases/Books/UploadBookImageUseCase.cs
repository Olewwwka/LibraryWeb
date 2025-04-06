using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Enums;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Lib.Application.UseCases.Books
{
    public class UploadBookImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public UploadBookImageUseCase(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task<string> ExecuteAsync(Guid id, IFormFile imageFile, CancellationToken cancellationToken)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            var fileName = await _fileService.SaveAsync(imageFile);
            var image = await _unitOfWork.BooksRepository.UpdateBookImageAsync(id, fileName, cancellationToken);

            return image;
        }
    }
}
