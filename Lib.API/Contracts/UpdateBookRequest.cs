using Lib.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Lib.API.Contracts
{
    public record UpdateBookRequest
    (
        string ISBN,
        string Name,
        Genre Genre,
        string Description
    );
}
