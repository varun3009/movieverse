using AutoMapper;
using Firebase.Storage;
using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using IMDBAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
namespace IMDBAPI.Repository
{
    public class MovieRepository:BaseRepository<MovieDB>,IMovieRepository
    {
        private readonly List<MovieDB> _movies;
        private readonly IMapper _mapper;
        public MovieRepository(IMapper mapper, IOptions<ConnectionString> options) : base(options.Value.IMDBDB)
        {
            _mapper = mapper;
            _movies = new List<MovieDB>();
        }
        public int AddMovie(MovieRequest movie)
        {
            
            int a=ExecuteInsertProcedure("Foundation.usp_insert_Movie", new
            {
                movie.Name,
                movie.Plot,
                Producer=movie.ProducerId,
                movie.Poster,
                Yor=movie.YearOfRelease,
                movie.Actors,
                movie.Genres
            });
            return a;
        }
        public void UpdateMovie(MovieRequest movie, int id)
        {
            int a = ExecuteStoredProcedure("Foundation.usp_update_Movie", new
            {
                Id=id,
                movie.Name,
                movie.Plot,
                Producer = movie.ProducerId,
                movie.Poster,
                Yor = movie.YearOfRelease,
                movie.Actors,
                movie.Genres
            });
        }
        public void DeleteMovie(int id)
        {
            ExecuteStoredProcedure("Foundation.usp_delete_Movie", new { Id = id });
        }
        public MovieDB MovieById(int id)
        {
            string query = @"SELECT id,
            NAME,
            plot,
            yearofrelease,
            producerid,
            poster,
            caption
            FROM   Foundation.Movies
            WHERE  id = @Id ";
            return Get(query, new {Id=id});
        }
        public List<int> GetMovieActors(int id)
        {
            string query = @"SELECT [actorid] Id
            FROM   Foundation.actors_movies
            WHERE  movieid = @id ";
            return GetAll(query,new { id = id }).Select(x=>x.Id).ToList();
        }
        public List<int> GetMovieGenres(int id)
        {
            string query = @"SELECT [genreid] Id
            FROM   Foundation.genres_movies
            WHERE  movieid = @id ";
            return GetAll(query, new { id = id }).Select(x => x.Id).ToList();
        }
        public List<MovieDB> GetAllMovies(int? year)
        {
            string query = @"SELECT id,
           NAME,
           plot,
           yearofrelease,
           producerid,
           poster,
           caption
            FROM   Foundation.Movies";
            if(year!=null)
                query +=" WHERE  yearofrelease = @year";
            return GetAll(query, new {year=year}).ToList();
        }

        public int UpdateMoviePoster(string poster, string caption, int id)
        {
            string query = @"
            UPDATE Foundation.Movies SET poster = @poster, caption = @caption WHERE Id = @id
            ";
            ExecuteQuery(query,new {poster = poster, id, caption});
            return id;
        }
        
    }
}
