using Lib.Core.Abstractions.Repositories;
using Lib.Application.Exceptions;
using Lib.Application.Abstractions.Authors;

namespace Lib.Application.UseCases.Authors
{
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
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

            var authorId = _unitOfWork.AuthorsRepository.RemoveAuthor(author);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return authorId;
        }
    }
}
