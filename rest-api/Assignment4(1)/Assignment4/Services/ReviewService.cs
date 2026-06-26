using AutoMapper;
using IMDBAPI.Exceptions;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using System.Text.RegularExpressions;
using IMDBAPI.Services.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using IMDBAPI.Models.DBModels;
using System;

namespace IMDBAPI.Services
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper,IMovieService movie)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _movieService = movie;
        }
        public List<ReviewResponse> GetReviews()
        {
            return _mapper.Map<List<ReviewResponse>>(_reviewRepository.GetAllReviews());
        }
        private bool isPureString(string p0)
        {
            var regex = new Regex(@"(?i)^[a-z.,\s]+$");
            bool res = regex.IsMatch(p0);
            return res;
        }
        public void AddReview(ReviewRequest review, int movieId)
        {

            if (string.IsNullOrWhiteSpace(review.Message))
                throw new InvalidInputException("Message is empty");
            if (!isPureString(review.Message))
                throw new InvalidInputException("Message is not valid");
            review.MovieId = movieId;
            try
            {
                _movieService.GetMovieById(review.MovieId);
            }
            catch(System.Exception ex)
            {
                throw new NotFoundException(ex.Message);
            }
            _reviewRepository.AddReview(review);
        }
        public void ValidateReviewId(int id)
        {
            var review = _reviewRepository.ReviewById(id);
            if (review == null)
                throw new NotFoundException("Review with given id doesn't exist");
        }
        public void ValidateMovieId(int movieId)
        {
            _movieService.ValidateMovieId(movieId);
        }
        public ReviewResponse GetReviewById(int id)
        {
            var review = _reviewRepository.ReviewById(id);
            if (review == null)
                throw new NotFoundException("Review with given id doesn't exist");
            return _mapper.Map<ReviewResponse>(review);
        }
        public void DeleteReview(int id, int movieId)
        {
            ValidateReviewId(id);
            ValidateMovieId(movieId);
            try
            {
                ReviewDB response = _reviewRepository.GetReview(movieId, id);
                if (response == null)
                    throw new Exception();
            }
            catch (Exception e)
            {
                throw new InvalidInputException("No review id exists under this movie");

            }
            _reviewRepository.DeleteReview(id);
        }
        public void UpdateReview(ReviewRequest review, int id, int movieId)
        {
            ValidateReviewId(id);
            ValidateMovieId(movieId);
            ValidateMovieId(review.MovieId);
            if (string.IsNullOrWhiteSpace(review.Message))
                throw new InvalidInputException("Message is empty");
            if (!isPureString(review.Message))
                throw new InvalidInputException("Message is not valid");
            try
            {
                ReviewDB response=_reviewRepository.GetReview(movieId, id);
                if (response == null)
                    throw new Exception();
            }
            catch(Exception e)
            {
                    throw new InvalidInputException("No review id exists under this movie");

            }
            _reviewRepository.UpdateReview(review, id);
        } 
        public List<ReviewResponse> GetReviewByMovieId(int movieId)
        {
            ValidateMovieId(movieId);
            
            return _mapper.Map<List<ReviewResponse>>(_reviewRepository.GetReviewsByMovieId(movieId));
        }
    }
}
