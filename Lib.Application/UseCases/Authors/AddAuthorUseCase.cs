using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Application.Models;

namespace Lib.Application.UseCases.Authors
{
    public class AddAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> ExecuteAsync(string name, string surname, DateTime birthday, string country, CancellationToken cancellationToken)
        {

            var author = new Author(name, surname, birthday, country);
            var authorEntity = _mapper.Map<AuthorEntity>(author);

            await _unitOfWork.AuthorsRepository.AddAuthorAsync(authorEntity, cancellationToken);

            return author;
        }
    }
}
