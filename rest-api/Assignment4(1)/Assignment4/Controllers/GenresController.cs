using IMDBAPI.Models.RequestModels;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    [Route("genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            
            return Ok(_genreService.GetGenres());
        }
        [HttpGet("{GenreId:int}")]
        public IActionResult GetGenreById([FromRoute] int GenreId) {
            
            return Ok(_genreService.GetGenreById(GenreId));
        }
        [HttpDelete("{GenreId:int}")]
        public IActionResult DeleteGenre([FromRoute] int GenreId)
        {
            _genreService.DeleteGenre(GenreId);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddGenre([FromBody] GenreRequest genreRequest)
        {
           int id= _genreService.AddGenre(genreRequest);
            return Created("[genres/]",id);
        }
        [HttpPut("{GenreId:int}")]
        public IActionResult UpdateGenre([FromBody]GenreRequest genreRequest, [FromRoute]int GenreId)
        {
            _genreService.UpdateGenre(genreRequest,GenreId); 
            return Ok();
        }

    }
}
