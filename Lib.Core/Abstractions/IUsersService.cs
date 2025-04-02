using Lib.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IUsersService
    {
        Task<User> Register(string name, string email, string password, CancellationToken cancellationToken);
        Task<(User, string accessToken, string RefreshToken)> Login(string email, string password, CancellationToken cancellationToken);
    }
}
