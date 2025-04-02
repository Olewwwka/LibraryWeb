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
    }
}
