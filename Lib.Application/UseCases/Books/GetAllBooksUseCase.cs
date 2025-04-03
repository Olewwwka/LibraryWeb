using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.UseCases.Books
{
    public class GetAllBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Book>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var bookEntities = await _unitOfWork.BooksRepository.GetAllBooksAsync(cancellationToken);

            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }
    }
}
