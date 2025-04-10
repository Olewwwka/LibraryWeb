using AutoMapper;
using Lib.Core.Entities;
using Lib.Core.Enums;
using Lib.Application.Exceptions;
using Book = Lib.Application.Models.Book;
using Lib.Core.Abstractions.Repositories;
using Lib.Application.Abstractions.Authors;
using Lib.Application.Abstractions.Books;
using Lib.Application.Contracts.Requests;

namespace Lib.Application.UseCases.Books
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> ExecuteAsync(AddBookRequest request, CancellationToken cancellationToken)
        {
            var existingBook = _unitOfWork.BooksRepository.GetBookByISBNAsync(request.ISBN, cancellationToken);

            if (existingBook is not null)
            {
                throw new ConflictException($"Book with ISBN {request.ISBN} already exists");
            }

            var bookModel = new Book(request.ISBN, request.Name, request.Genre, request.Description, request.AuthorId)
            {
                ImagePath = "default_image.jpg",
                BorrowTime = DateTime.MinValue,
                ReturnTime = DateTime.MinValue
            };

            var bookEntity = _mapper.Map<BookEntity>(bookModel);
            var addedBook = await _unitOfWork.BooksRepository.AddBookAsync(bookEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return _mapper.Map<Book>(addedBook);
        }
    }
}
