using AutoMapper;
using Lib.Application.Abstractions.Users;
using Lib.Application.Models;
using Lib.Core.Abstractions.Repositories;

namespace Lib.Application.UseCases.Users
{
    public class GetUsersBorrowedBooksUseCase : IGetUsersBorrowedBooksUseCase
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
