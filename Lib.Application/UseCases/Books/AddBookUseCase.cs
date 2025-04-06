using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Enums;
using Lib.Core.Exceptions;
using Lib.Application.Models;
using Book = Lib.Application.Models.Book;

namespace Lib.Application.UseCases.Books
{
    public class AddBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> ExecuteAsync(string isbn, string name, Genre genre, string description, Guid authorId, CancellationToken cancellationToken)
        {
            var bookModel = new Book(isbn, name, genre, description, authorId)
            {
                ImagePath = "default_image.jpg",
                BorrowTime = null,
                ReturnTime = null
            };

            var books = await _unitOfWork.BooksRepository.GetAllBooksAsync(cancellationToken);

            if(books.Any(book => book.Name == name))
            {
                throw new ConflictException($"Book with name {name} already exists");
            }

            var bookEntity = _mapper.Map<BookEntity>(bookModel);
            var addedBook = await _unitOfWork.BooksRepository.AddBookAsync(bookEntity, cancellationToken);

            return _mapper.Map<Book>(addedBook);
        }
    }
}
