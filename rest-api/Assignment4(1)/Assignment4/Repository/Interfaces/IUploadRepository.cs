using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IMDBAPI.Repository.Interfaces
{
    public interface IUploadRepository
    {
        Task<string> Upload(IFormFile file);

        public string GenerateDownloadUrl(string objectKey, int durationMinutes = 30);
    }
}
