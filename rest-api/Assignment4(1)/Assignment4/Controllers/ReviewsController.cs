using IMDBAPI.Models.RequestModels;
using IMDBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDBAPI.Controllers
{
    [ApiController]
    [Route("movies/{MovieId:int}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
    
        [HttpPost]
        public IActionResult AddReview([FromBody] ReviewRequest reviewRequest, [FromRoute]int MovieId)
        {
            _reviewService.AddReview(reviewRequest,MovieId);
            return Created("[/reviews/]",null);
        }
        [HttpPut("{ReviewId:int}")]
        public IActionResult UpdateReview([FromBody] ReviewRequest reviewRequest, [FromRoute] int ReviewId, [FromRoute] int MovieId)
        {
            _reviewService.UpdateReview(reviewRequest, ReviewId,MovieId);
            return Ok();
        }
        [HttpDelete("{ReviewId:int}")]
        public IActionResult DeleteReview([FromRoute]int ReviewId, [FromRoute]int MovieId)
        {
            _reviewService.DeleteReview(ReviewId,MovieId);
            return Ok();
        }
        [HttpGet("~/reviews/{ReviewId:int}")]
        public IActionResult GetReviewById([FromRoute]int ReviewId)
        {

            return Ok(_reviewService.GetReviewById(ReviewId));
        }
        [HttpGet]
        public IActionResult GetReviews([FromRoute]int MovieId)
        {
            return Ok(_reviewService.GetReviewByMovieId(MovieId));
        }

        [HttpGet("~/reviews")]
        public IActionResult GetAll()
        {
            return Ok(_reviewService.GetReviews());
        }

    }
}
