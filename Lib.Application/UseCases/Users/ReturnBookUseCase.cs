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
    public class ReturnBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReturnBookUseCase(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> ExecuteAsync(Guid bookId, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.UsersRepository.ReturnBookAsync(bookId, cancellationToken);
            var book = _mapper.Map<Book>(bookEntity);

            return book;
        }
    }
}
