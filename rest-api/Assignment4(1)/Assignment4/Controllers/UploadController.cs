using System.Threading.Tasks;
using IMDBAPI.Services;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    [Route("upload")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            return Ok(await _uploadService.Upload(file));
        }
    }
}
