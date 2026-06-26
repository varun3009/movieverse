using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IMDBAPI.Services.Interfaces
{
    public interface IUploadService
    {
        public Task<string> Upload(IFormFile file);
        public string GetFileAsync(string FileName);
    }
}
