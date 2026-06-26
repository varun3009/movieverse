using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using System.Linq;
using System.Collections.Generic;

namespace IMDBAPI.Services.Interfaces
{
    public interface IReviewService
    {
        List<ReviewResponse> GetReviews();
        public void AddReview(ReviewRequest Review,int MovieId);
        public ReviewResponse GetReviewById(int id);
        public void UpdateReview(ReviewRequest Review, int id,int MovieId);
        public void DeleteReview(int id,int MovieId);
        public List<ReviewResponse> GetReviewByMovieId(int mid);
    }
}
