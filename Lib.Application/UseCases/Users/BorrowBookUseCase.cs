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
    public class BorrowBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BorrowBookUseCase(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> ExecuteAsync(Guid userId, Guid bookId, DateTime borrowTime, DateTime returnTime, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.UsersRepository.BorrowBookAsync(userId, bookId, borrowTime, returnTime, cancellationToken);
            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
