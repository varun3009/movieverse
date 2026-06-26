using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using System.Linq;
using System.Collections.Generic;
using IMDBAPI.Models.DBModels;
namespace IMDBAPI.Repository.Interfaces
{
    public interface IReviewRepository
    {

        void AddReview(ReviewRequest review);
        
        void UpdateReview(ReviewRequest review, int id);
        void DeleteReview(int id);
        ReviewDB ReviewById(int id);
        List<ReviewDB> GetAllReviews();
        List<ReviewDB> GetReviewsByMovieId(int movieId);
        ReviewDB GetReview(int movieId, int id);
    }
}
