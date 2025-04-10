using AutoMapper;
using Lib.Core.Entities;
using Lib.Application.Models;
using Lib.Application.Exceptions;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Abstractions.Authors;
using Lib.Application.Contracts.Responses;
using Lib.Application.Contracts.Requests;
using Lib.Core.Abstractions;

namespace Lib.Application.UseCases.Authors
{
    public class AddAuthorUseCase : IAddAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> ExecuteAsync(AddAuthorRequest request, CancellationToken cancellationToken)
        {

            var author = new Author(request.Name, request.Surname, request.Birthday, request.Country);
            var authorEntity = _mapper.Map<AuthorEntity>(author);

            await _unitOfWork.AuthorsRepository.AddAuthorAsync(authorEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return author;
        }
    }
}
