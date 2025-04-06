using Lib.Core.Abstractions;
using Lib.Core.Exceptions;

namespace Lib.Application.UseCases.Authors
{
    public class DeleteAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {

            var author = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(id, cancellationToken);

            if (author == null)
            {
                throw new NotFoundException($"Author with id {id} not found");
            } 

            var authorId = await _unitOfWork.AuthorsRepository.DeleteAuthorAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return authorId;
        }
    }
}
