using Microsoft.AspNetCore.Http;

namespace Lib.Application.Contracts.Requests
{
    public record UploadBookImageRequest
    (
        Guid id,
        IFormFile imageFile
    );
}
