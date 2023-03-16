using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileUpload
{
    public interface IFIleService
    {
        Task<(string Server, string FilePath)> Upload(IFormFile file);
        Task Remove(string path);
    }
}
