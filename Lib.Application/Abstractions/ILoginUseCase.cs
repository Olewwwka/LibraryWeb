using Lib.Application.Contracts.Requests;
using Lib.Application.Contracts.Responses;
using Lib.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Application.Abstractions
{
    public interface ILoginUseCase : IUseCase<LoginRequest, LoginResponse> { }
}
