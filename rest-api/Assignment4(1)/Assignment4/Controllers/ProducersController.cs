using IMDBAPI.Models.RequestModels;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    [Route("producers")]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;
        public ProducersController(IProducerService producerService) {
            _producerService = producerService;
        }

        [HttpGet]
        public IActionResult GetProducers()
        {

            return Ok(_producerService.GetProducers());
        }
        [HttpGet("{ProducerId:int}")]
        public IActionResult GetProducerById([FromRoute] int ProducerId)
        {
            return Ok(_producerService.GetProducerById(ProducerId));
        }
        [HttpDelete("{ProducerId:int}")]
        public IActionResult DeleteProducer([FromRoute] int ProducerId)
        {
            _producerService.DeleteProducer(ProducerId);
            return Ok();
        }
        [HttpPost]
        public IActionResult AddProducer([FromBody] ProducerRequest producerRequest)
        {
            int id=_producerService.AddProducer(producerRequest);
            return Created("[/producer/]",id);
        }
        [HttpPut("{ProducerId:int}")]
        public IActionResult UpdateProducer([FromBody] ProducerRequest producerRequest, [FromRoute] int ProducerId)
        {
            _producerService.UpdateProducer(producerRequest, ProducerId);
            return Ok();
        }
    }
}
