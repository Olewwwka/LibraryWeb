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
    }
}
