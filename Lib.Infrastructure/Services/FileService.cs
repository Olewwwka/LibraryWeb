using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Lib.Core.Abstractions;

namespace Lib.Application
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;
        private string[] allowedExtensions = [".jpg", ".jpeg", ".png"];
        private string defaultImagePath => "default_image.jpg";
        private const long MaxFileSize = 5 * 1024 * 1024;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveAsync(IFormFile imageFile)
        {
            if (imageFile is null)
            {
                return defaultImagePath;
            }

            if (imageFile.Length > MaxFileSize)
            {
                throw new ArgumentException($"File size cannot exceed {MaxFileSize / 1024 / 1024}MB");
            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(imageFile.FileName);
            if (!allowedExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedExtensions)} are allowed");
            }

            var fileName = $"{Guid.NewGuid()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);

            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            return fileName;
        }

        public void Delete(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }

            if (fileNameWithExtension == defaultImagePath)
            {
                return;
            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }

            File.Delete(path);
        }
    }
} 