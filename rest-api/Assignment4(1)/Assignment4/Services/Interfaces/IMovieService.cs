using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using Microsoft.AspNetCore.Http;


namespace IMDBAPI.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieResponse> GetMovies(int? year);
        List<MovieResponse> GetMovies();
        public int AddMovie(MovieRequest Movie);
        public MovieResponse GetMovieById(int id);
        public void UpdateMovie(MovieRequest Movie, int id);
        public void DeleteMovie(int id);
        public void ValidateMovieId(int id);
        
    }
}
