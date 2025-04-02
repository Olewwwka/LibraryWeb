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
    }
}
