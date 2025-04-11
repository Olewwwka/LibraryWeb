using AutoMapper;
using Lib.Core.Abstractions.Repositories;
using Lib.Core.Enums;
using Lib.Application.Exceptions;
using Lib.Application.Models;
using Lib.Core.Entities;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Books
{
    public class UpdateBookInfoUseCase : IUpdateBookInfoUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBookInfoUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> ExecuteAsync(UpdateBookInfoRequest request, CancellationToken cancellationToken)
        {
            var bookEntity = await _unitOfWork.BooksRepository.GetBookByIdAsync(request.Id, cancellationToken);
            if (bookEntity is null)
            {
                throw new NotFoundException($"Book with ID {request.Id} is not found");
            }

            if (bookEntity.ISBN != request.ISBN)
            {
                var existingBook = await _unitOfWork.BooksRepository.GetBookByISBNAsync(request.ISBN, cancellationToken);
                if (existingBook is not null)
                {
                    throw new ConflictException($"Book with ISBN {request.ISBN} already exists");
                }
            }

            _mapper.Map(request, bookEntity);

            _unitOfWork.BooksRepository.UpdateBook(bookEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<Book>(bookEntity);
        }
    }
}
