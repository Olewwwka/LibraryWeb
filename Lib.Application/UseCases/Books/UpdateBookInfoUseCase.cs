using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Enums;
using Lib.Core.Exceptions;

namespace Lib.Application.UseCases.Books
{
    public class UpdateBookInfoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBookInfoUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> ExecuteAsync(Guid id, string isbn, string name, Genre genre, string description, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.BooksRepository.GetBookByIdAsync(id, cancellationToken);

            if (bookEntity == null)
            {
                throw new NotFoundException($"Book with ID {id} is not found");
            }

            var guid = await _unitOfWork.BooksRepository.UpdateBookAsync(id, isbn, name, genre, description, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return guid;
        }
    }
}
