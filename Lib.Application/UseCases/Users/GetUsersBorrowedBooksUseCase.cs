using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.UseCases.Users
{
    public class GetUsersBorrowedBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUsersBorrowedBooksUseCase(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<Book>> ExecuteAsync(Guid userId, CancellationToken cancellationToken)
        {
            var bookEntities = await _unitOfWork.UsersRepository.GetUserBorrowedBooksAsync(userId, cancellationToken);
            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }
    }
}
