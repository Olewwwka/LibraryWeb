using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Contracts.Requests
{
    public record UpdateAuthorRequest
    (
        Guid Id,
        string Name, 
        string Surname,
        string Country,
        DateTime Birthday
    );
}
