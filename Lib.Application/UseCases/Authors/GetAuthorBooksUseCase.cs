﻿using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Application.Models;

namespace Lib.Application.UseCases.Authors
{
    public class GetAuthorBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAuthorBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        {
            var bookEntities = await _unitOfWork.AuthorsRepository.GetBooksByAuthorAsync(id, cancellationToken);
            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }
    }
}
