using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Services
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthorsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Author> AddAuthor(string name, string surname, DateTime birthday, string country, CancellationToken cancellationToken)
        {
            var author = new Author(name, surname, birthday, country);
            var authorEntity = _mapper.Map<AuthorEntity>(author);

            await _unitOfWork.AuthorsRepository.AddAuthorAsync(authorEntity, cancellationToken);

            return author;
        }

        public async Task<Author> GetAuthorById(Guid id, CancellationToken cancellationToken)
        {
            var authorEntity = await _unitOfWork.AuthorsRepository.GetAuthrorByIdAsync(id, cancellationToken);
            var author = _mapper.Map<Author>(authorEntity);

            return author;
        }

        public async Task<List<Author>> GetAllAuthors(CancellationToken cancellationToken)
        {
            var authorEntities = await _unitOfWork.AuthorsRepository.GetAllAuthorsAsync(cancellationToken);
            var authors = _mapper.Map<List<Author>>(authorEntities);

            return authors;
        }

        public async Task<Guid> UpdateAuthorInfo(Guid id, string name, string surname, string country, DateTime birthday, CancellationToken cancellationToken)
        {
            var authorId = await _unitOfWork.AuthorsRepository.UpdateAuthorAsync(id, name, surname, birthday, country, cancellationToken);

            return authorId;
        }

        public async Task<Guid> DeleteAuthor(Guid id, CancellationToken cancellationToken)
        {
            var authorId = await _unitOfWork.AuthorsRepository.DeleteAuthorAsync(id, cancellationToken);

            return authorId;
        }

        public async Task<List<Book>> GetAuthorBooks(Guid id, CancellationToken cancellationToken)
        {
            var bookEntities = await _unitOfWork.AuthorsRepository.GetBooksByAuthorAsync(id, cancellationToken);
            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }
    }
}
