using Microsoft.AspNetCore.Http;

namespace Lib.Core.Abstractions
{
    public interface IFileService
    {
        Task<string> SaveAsync(IFormFile imageFile);
        void Delete(string fileNameWithExtension);
    }
} 