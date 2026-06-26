using System.Threading.Tasks;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("movies")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpGet]
        public IActionResult GetMovies([FromQuery] int? year)
        {

            return Ok(_movieService.GetMovies(year));
        }
        [HttpGet("{MovieId:int}")]
        public IActionResult GetMovieById([FromRoute] int MovieId)
        {
            return Ok(_movieService.GetMovieById(MovieId));
        }
        [HttpDelete("{MovieId:int}")]
        public IActionResult DeleteMovie([FromRoute] int MovieId)
        {
            _movieService.DeleteMovie(MovieId);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddMovie([FromBody] MovieRequest movieRequest)
        {
            int id=_movieService.AddMovie(movieRequest);
            return Created("[/movies/]", id);
        }
        [HttpPut("{MovieId:int}")]
        public IActionResult UpdateMovie([FromBody] MovieRequest movieRequest, [FromRoute] int MovieId)
        {
            _movieService.UpdateMovie(movieRequest, MovieId);
            return Ok();
        }
    }
}
