using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Repository.Interfaces;
using Moq;

namespace IMDB.Tests.Mock
{
    public class ReviewsRepositoryMock
    {
        private static readonly List<ReviewDB> _reviews = new List<ReviewDB>{
            new ReviewDB{
                Id=1,
                MovieId=1,
                Message="good"
            },
            new ReviewDB
            {
                Id=2,
                MovieId=2,
                Message="good"
            }
        };
        public static Moq.Mock<IReviewRepository> ReviewRepositoryMoq = new Moq.Mock<IReviewRepository>();

        public ReviewsRepositoryMock() { }
        public static void MockAll()
        {
            ReviewRepositoryMoq.Setup(foo => foo.AddReview(It.IsAny<ReviewRequest>()));
            ReviewRepositoryMoq.Setup(foo => foo.DeleteReview(It.IsAny<int>()));
            ReviewRepositoryMoq.Setup(foo => foo.UpdateReview(It.IsAny<ReviewRequest>(), It.IsAny<int>()));
            ReviewRepositoryMoq.Setup(foo => foo.GetAllReviews()).Returns(_reviews);
            ReviewRepositoryMoq.Setup(foo => foo.GetReview(It.IsAny<int>(), It.IsAny<int>())).Returns((int x,int y)=>_reviews.FirstOrDefault(z=>z.Id==y && z.MovieId==x));
            ReviewRepositoryMoq.Setup(foo => foo.ReviewById(It.IsAny<int>())).Returns((int x) => _reviews.FirstOrDefault(y => y.Id == x));
            ReviewRepositoryMoq.Setup(foo => foo.GetReviewsByMovieId(It.IsAny<int>())).Returns((int x) => _reviews.Where(y => y.MovieId == x).ToList());

        }
    }
        
}
