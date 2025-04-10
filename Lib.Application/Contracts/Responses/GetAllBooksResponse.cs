using Lib.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Contracts.Responses
{
    public record GetAllBooksResponse
    (
        List<Book> Books,
        int TotalPages
    );
    
}
