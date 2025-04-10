using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Books
{
    public class UploadBookImageUseCase : IUploadBookImageUseCase
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

        public async Task<string> ExecuteAsync(UploadBookImageRequest request, CancellationToken cancellationToken)
        {
            if (request.imageFile == null || request.imageFile.Length == 0)
            {
                throw new ArgumentNullException(nameof(request.imageFile));
            }

            var fileName = await _fileService.SaveAsync(request.imageFile);

            var image = await _unitOfWork.BooksRepository.UpdateBookImageAsync(request.id, fileName, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return image;
        }
    }
}
