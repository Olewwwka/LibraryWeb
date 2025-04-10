using Lib.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Contracts.Requests
{
    public record AddBookRequest
    (
        string ISBN,
        string Name,
        Genre Genre,
        string Description,
        Guid AuthorId
    );
}
