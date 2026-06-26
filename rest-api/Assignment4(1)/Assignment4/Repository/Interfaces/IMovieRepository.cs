using IMDBAPI.Models.DBModels;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDBAPI.Repository.Interfaces
{
    public interface IMovieRepository
    {

        int AddMovie(MovieRequest movie);
       
        void UpdateMovie(MovieRequest movie, int id);
        void DeleteMovie(int id);
        MovieDB MovieById(int id);
        List<MovieDB> GetAllMovies(int? year);
        List<int> GetMovieActors(int id);
        List<int> GetMovieGenres(int id);
        int UpdateMoviePoster(string poster, string caption, int id);
    }
}
