using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Application.FileUpload
{
    public class FileUploadService : IFIleService
    {

        private readonly IConfiguration _configuration;
        public FileUploadService(IConfiguration configuration) => _configuration = configuration;

        public async Task Remove(string path)
        {
            string rootFolder = Directory.GetCurrentDirectory();

            File.Delete(Path.Combine(
                Directory.GetParent(rootFolder) + "/WebApplication", "wwwroot/images", path));
        }
        public async Task<(string Server, string FilePath)> Upload(IFormFile file)
        {
            List<string> allowedTypes = new List<string>()
              {
                  ".jpg",
                  ".jpeg",
                  ".JPG",
                  ".png",
                  ".gif"
              };

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExtension = Path.GetExtension(file.FileName);

            if (!allowedTypes.Contains(fileExtension))
                throw new Exception("Invalid file format.");

            string fullName = $"{Guid.NewGuid()}_{fileName}{fileExtension}";

            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()) + "/WebApplication", "wwwroot/images", fullName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return ($"{_configuration.GetSection("Server").Value}/images/{fullName}", path);

            //   public static IEnumerable<string> AllowExtensions => new List<string> { ".jpeg", ".jpg", ".png" };
        }
    }
}
