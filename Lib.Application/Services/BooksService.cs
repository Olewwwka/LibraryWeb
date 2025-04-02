using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Enums;
using Lib.Core.Models;

namespace Lib.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BooksService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> AddBook(string isbn, string name, Genre genre, string description, Guid authorId, CancellationToken cancellationToken)
        {
            var bookModel = new Book(isbn, name, genre, description, authorId);

            if (bookModel == null) throw new Exception(); //====================

            var bookEntity = _mapper.Map<BookEntity>(bookModel);

            var book = await _unitOfWork.BooksRepository.AddBookAsync(bookEntity, cancellationToken);

            return bookModel;
        }

        public async Task<List<Book>> GetAllBooks(CancellationToken cancellationToken)
        {
            var bookEntities = await _unitOfWork.BooksRepository.GetAllBooksAsync(cancellationToken);

            var books = _mapper.Map<List<Book>>(bookEntities);

            return books;
        }
    }
}