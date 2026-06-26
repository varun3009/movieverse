using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("actors")]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }
        [HttpGet]
        public IActionResult GetActors()
        {
            var actors = _actorService.GetActors();
            return Ok(actors);
        }
        [HttpGet("{ActorId:int}")]
        public IActionResult GetActorById([FromRoute] int ActorId)
        {
            var actor=_actorService.GetActorById(ActorId);
            return Ok(actor);
        }
        [HttpPost]
        public IActionResult AddActor([FromBody] ActorRequest actorRequest)
        {
            int id=_actorService.AddActor(actorRequest);
            return Created("AddActor",id);
        }
        [HttpPut("{ActorId:int}")]
        public IActionResult UpdateActor([FromRoute] int ActorId, [FromBody] ActorRequest actorRequest)
        {
            _actorService.UpdateActor(actorRequest,ActorId);
            return Ok();
        }
        [HttpDelete("{ActorId:int}")]
        public IActionResult DeleteActor([FromRoute] int ActorId)
        {
            _actorService.DeleteActor(ActorId);
            return Ok();
        }
    }
}
