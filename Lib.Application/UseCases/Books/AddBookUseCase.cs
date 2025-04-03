using AutoMapper;
using Lib.Core.Abstractions;
using Lib.Core.Entities;
using Lib.Core.Enums;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var bookModel = new Book(isbn, name, genre, description, authorId);

            if (bookModel == null) throw new Exception(); //====================

            var bookEntity = _mapper.Map<BookEntity>(bookModel);

            var book = await _unitOfWork.BooksRepository.AddBookAsync(bookEntity, cancellationToken);

            return bookModel;
        }
    }
}
