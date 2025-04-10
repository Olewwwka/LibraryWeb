using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Core.Abstractions
{
    public interface IUseCase {}

    public interface IUseCase<TResponse> : IUseCase
    {
        Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IUseCase<in TRequest, TResponse> : IUseCase
    {
        Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
    }
}
