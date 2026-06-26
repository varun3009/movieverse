using IMDBAPI.Exceptions;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace IMDBAPI.Services
{
    public class UploadService:IUploadService
    {
        private readonly IUploadRepository _uploadrepository;
        public UploadService(IUploadRepository uploadRepositroy) {
            _uploadrepository = uploadRepositroy;
        }
        public async Task<string> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new InvalidInputException("file not selected");
            var task = await _uploadrepository.Upload(file);
            return task;
        }

        public string GetFileAsync(string FileName)
        {
            return _uploadrepository.GenerateDownloadUrl(FileName);
        }
    }
}
