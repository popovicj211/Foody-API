using Microsoft.AspNetCore.Http;

namespace Application.FileUpload
{
    public interface IFIleService
    {
        Task<(string Server, string FilePath)> Upload(IFormFile file);
        Task Remove(string path);
    }
}
