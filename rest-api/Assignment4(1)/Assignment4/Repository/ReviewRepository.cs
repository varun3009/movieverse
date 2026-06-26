using AutoMapper;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace IMDBAPI.Repository
{
    public class ReviewRepository:BaseRepository<ReviewDB>,IReviewRepository
    {
        private readonly IMapper _mapper;
        public ReviewRepository(IMapper mapper, IOptions<ConnectionString> options) : base(options.Value.IMDBDB)
        {
            _mapper = mapper;
            
        }
        public void AddReview(ReviewRequest review)
        {
            var query = @"INSERT INTO Foundation.Reviews
                        (message,
                         movieid)
            VALUES     (@Message,
                        @MovieId) ";
            ExecuteQuery(query, review);
            
        }
        public void UpdateReview(ReviewRequest review, int id)
        {
            ReviewDB reviewdb = _mapper.Map<ReviewDB>(review);
            reviewdb.Id = id;
            var query = @"UPDATE Foundation.Reviews
            SET    message = @Message,
                   movieid = @MovieId
            WHERE  id = @Id ";
            ExecuteQuery(query, reviewdb);
            
        }
        public void DeleteReview(int id)
        {
            var query = @"DELETE FROM Foundation.Reviews
            WHERE  id = @id ";
            ExecuteQuery(query, new { id = id });
            
        }
        public ReviewDB ReviewById(int id)
        {
            var query = @"SELECT *
            FROM   Foundation.Reviews(nolock)
            WHERE  id = @id ";
            return Get(query, new {id = id});
        }
        public List<ReviewDB> GetAllReviews()
        {

            var query = @"SELECT *
            FROM   Foundation.Reviews(nolock) ";
            return  GetAll(query).ToList();
        }
        public List<ReviewDB>  GetReviewsByMovieId(int movieId)
        {

            var query = @"SELECT *
            FROM   Foundation.Reviews(nolock)
            WHERE  movieid = @Id ";
            return GetAll(query, new { Id = movieId }).ToList();
        }
        public ReviewDB GetReview(int movieId,int id)
        {
            var query = @"SELECT *
            FROM   Foundation.Reviews(nolock)
            WHERE  movieid = @mid and id=@id";
            return Get(query, new { mid = movieId, id = id });
        }
    }
}
