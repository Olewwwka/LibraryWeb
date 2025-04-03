using Lib.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IAuthorsService
    {
        Task<Author> AddAuthor(string name, string surname, DateTime birthday, string country, CancellationToken cancellationToken);
        Task<Author> GetAuthorById(Guid id, CancellationToken cancellationToken);
        Task<List<Author>> GetAllAuthors(CancellationToken cancellationToken);
        Task<Guid> UpdateAuthorInfo(Guid id, string name, string surname, string country, DateTime birthday, CancellationToken cancellationToken);
        Task<Guid> DeleteAuthor(Guid id, CancellationToken cancellationToken);
        Task<List<Book>> GetAuthorBooks(Guid id, CancellationToken cancellationToken);
    }
}
