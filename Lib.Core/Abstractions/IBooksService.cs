using Lib.Core.Enums;
using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IBooksService
    {
        Task<Book> AddBook(string isbn, string name, Genre genre, string description, Guid authorId, CancellationToken cancellationToken);
        Task<List<Book>> GetAllBooks(CancellationToken cancellationToken);
        Task<Book> GetBookById(Guid id, CancellationToken cancellationToken);
        Task<Book> GetBookByISBN(string isbn, CancellationToken cancellationToken);
        Task<Guid> UpdateBookInfo(Guid id, string isbn, string name, Genre genre, string description, CancellationToken cancellationToken);
        Task<Guid> DeleteBook(Guid id, CancellationToken cancellationToken);
    }
}
