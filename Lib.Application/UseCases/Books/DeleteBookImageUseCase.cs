using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Enums;
using Lib.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Lib.Application.UseCases.Books
{
    public class DeleteBookImageUseCase
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

        public async Task<string> ExecuteAsync(Guid id, string fileName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            _fileService.Delete(fileName);

            var result = await _unitOfWork.BooksRepository.DeleteBookImageAsync(id, cancellationToken);

            return result;
        }
    }
}
