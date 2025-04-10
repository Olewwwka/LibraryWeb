using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Contracts.Responses
{
    public record LoginResponse
    (
        Guid Id,
        string Name,
        string Email,
        string Role,
        string AccessToken,
        string RefreshToken
    );
}
