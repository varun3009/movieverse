using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Services.Interfaces;
using System.Text.RegularExpressions;
using IMDBAPI.Repository.Interfaces;
using System.Xml.Linq;
using System.Globalization;
using IMDBAPI.Exceptions;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Http;
using Firebase.Storage;
using AutoMapper;
using IMDBAPI.Models.DBModels;
using System.Security.Cryptography;
using StackExchange.Redis;
namespace IMDBAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IProducerService _producerService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly RedisService<MovieDB> _redisService;
        private readonly IMessageService<MovieDB> _messageService;
        private readonly IUploadService _uploadService;

        public MovieService(IActorService actorService, IProducerService producerService, IMovieRepository movieRepository, IGenreService genreService, IMapper mapper, IConnectionMultiplexer connectionMultiplexer, IMessageService<MovieDB> messageService, IUploadService uploadService)
        {
            _movieRepository = movieRepository;
            _actorService = actorService;
            _producerService = producerService;
            _genreService = genreService;
            _mapper = mapper;
            _redisService = new RedisService<MovieDB>(connectionMultiplexer);
            _messageService = messageService;
            _uploadService = uploadService;
        }
        public void CheckEmpty(String title, string actors, string plot, string genre, string poster)
        {
            var l = title.Length;
            var p = plot.Length;
            if (string.IsNullOrWhiteSpace(title))
                throw new InvalidInputException("Title is Empty");
            if (string.IsNullOrWhiteSpace(plot))
                throw new InvalidInputException("Plot is Empty");
            if (string.IsNullOrWhiteSpace(actors))
                throw new InvalidInputException("actors is Empty");
            if (string.IsNullOrWhiteSpace(genre))
                throw new InvalidInputException("genre is Empty");
            if (string.IsNullOrWhiteSpace(poster))
                throw new InvalidInputException("poster is Empty");

        }
        public void ValidateMovie(MovieRequest movie)
        {
            CheckEmpty(movie.Name, movie.Actors, movie.Plot, movie.Genres, movie.Poster);
            if (!isValidPlot(movie.Plot))
                throw new InvalidInputException("Invalid Plot");
            if (!isValidTitle(movie.Name))
                throw new InvalidInputException("Title is not valid");
            if (movie.YearOfRelease<1800)
                throw new InvalidInputException("Invalid YearOfRelease");
            try
            {
                _producerService.GetProducerById(movie.ProducerId);
                ValidateActors(movie.Actors);
                ValidateGenres(movie.Genres);

            }
            catch (Exception e)
            {
                throw new InvalidInputException(e.Message);
            }
        }
        public int AddMovie(MovieRequest movie)
        {
            ValidateMovie(movie);
            var id = _movieRepository.AddMovie(movie);
            MovieDB movieDb = new MovieDB();
            movieDb.Id = id;
            movieDb.Poster = movie.Poster;
            _messageService.PostMessageAsync(movieDb, "update_poster");
            return id;

        }
        public void ValidateActors(string actors)
        {
            var actorsId = ArrayOfNumbers(actors,"Actors");
            foreach (var i in actorsId)
            {
                _actorService.GetActorById(i);
            }

        }
        public void ValidateGenres(String genres)
        {
            var genresId = ArrayOfNumbers(genres,"Genres");
            foreach (var i in genresId)
            {
                _genreService.GetGenreById(i);
            }
        }
        public List<int> ArrayOfNumbers(String p0,String type)
        {
            List<int> ans = new List<int>();
            String[] arr = p0.Split(' ');
            for (int i = 0; i < arr.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(arr[i]))
                {
                    try
                    {
                        ans.Add(int.Parse(arr[i]));
                    }
                    catch (Exception e)
                    {
                        throw new InvalidInputException("Invalid " +type+" Input");
                    }
                }
            }
            return ans;
        }
        public List<MovieResponse> GetMovies(int? year)
        {
            var movies = _movieRepository.GetAllMovies(year);
            List<MovieResponse> list=new List<MovieResponse> ();
            foreach (var movie in movies)
            {
                int pid = movie.ProducerId;
                var movieResponse = _mapper.Map<MovieResponse>(movie);
                movieResponse.Poster = _uploadService.GetFileAsync(movieResponse.Poster);
                movieResponse.Producer=_producerService.GetProducerById(pid);
                AddActorsGenres(movieResponse, movie.Id);
                list.Add(movieResponse);
            }
                return list;

        }
        private bool isValidTitle(String title)
        {
            var regex = new Regex(@"^[0-9A-Za-z.,\s]+$");
            return regex.IsMatch(title);
        }
        private bool isValidPlot(String plot)
        {
            var regex = new Regex(@"[A-Za-z]+");
            return regex.IsMatch(plot);

        }

        public void ValidateMovieId(int id)
        {
            try
            {
                var movie = _movieRepository.MovieById(id);
                if (movie == null)
                    throw new NotFoundException("Movie with given id doesn't exist");
            }
            catch(Exception e)
            {
                    throw new NotFoundException("Movie with given id doesn't exist");

            }
        }

        public void DeleteMovie(int id)
        {

            ValidateMovieId(id);
            _movieRepository.DeleteMovie(id);
            _redisService.DeleteAsync($"movie-{id}").ConfigureAwait(false).GetAwaiter().GetResult();

        }
        public MovieResponse GetMovieById(int id)
        {
            var movie = _redisService.GetValueAsync($"movie-{id}").ConfigureAwait(false).GetAwaiter().GetResult();
            if (movie == null)
            {
                movie = _movieRepository.MovieById(id);
                if(movie is not null)
                {
                    _redisService.SetAsync($"movie-{id}", movie).ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }
            if (movie == null)
                throw new NotFoundException("Movie with given id doesn't exist");
            int pid = movie.ProducerId;
            var movieResponse = _mapper.Map<MovieResponse>(movie);
            movieResponse.Poster = _uploadService.GetFileAsync(movie.Poster);
            movieResponse.Producer = _producerService.GetProducerById(pid);
            AddActorsGenres(movieResponse, movie.Id);
            return movieResponse;
        }
        public List<MovieResponse> GetMovies()
        {
            var movies = _movieRepository.GetAllMovies(null);
            List<MovieResponse> list=new List<MovieResponse> ();
            foreach (var movie in movies)
            {
                int pid = movie.ProducerId;
                var movieResponse = _mapper.Map<MovieResponse>(movie);
                movieResponse.Producer = _producerService.GetProducerById(pid);
                AddActorsGenres(movieResponse, movie.Id);
                list.Add(movieResponse);
            }
            return list;
        }
        public void UpdateMovie(MovieRequest movie, int id)
        {
            ValidateMovieId(id);
            ValidateMovie(movie);
            if(movie.Poster.Contains("https"))
            {
                movie.Poster = _movieRepository.MovieById(id).Poster;
            }
            _movieRepository.UpdateMovie(movie, id);
            _redisService.DeleteAsync($"movie-{id}").ConfigureAwait(false).GetAwaiter().GetResult();

            MovieDB movieDb = new MovieDB();
            movieDb.Id = id;
            movieDb.Poster = movie.Poster;
            _messageService.PostMessageAsync(movieDb, "update_poster");
        }
        public void AddActorsGenres(MovieResponse movie, int id)
        {
            var actors = _movieRepository.GetMovieActors(id);
            List<ActorResponse> actorResponses = new List<ActorResponse>();
            foreach (var actor in actors)
                actorResponses.Add(_actorService.GetActorById(actor));
            List<GenreResponse> genreResponses = new List<GenreResponse>();
            foreach (var genre in _movieRepository.GetMovieGenres(id))
                genreResponses.Add(_genreService.GetGenreById(genre));
            movie.Actors = actorResponses;
            movie.Genres = genreResponses;

        }
        

    }
}
