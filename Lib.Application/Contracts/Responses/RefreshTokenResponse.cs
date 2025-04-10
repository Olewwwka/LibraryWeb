using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Contracts.Responses
{
    public record RefreshTokenResponse
    (
        string RefreshToken,
        string AccessToken
    );
    
}
