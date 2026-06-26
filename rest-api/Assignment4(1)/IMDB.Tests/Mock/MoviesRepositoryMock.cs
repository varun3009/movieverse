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
    public class MoviesRepositoryMock
    {
        private static readonly List<MovieDB> ListOfMovies = new List<MovieDB>()
        {
           new MovieDB{
                Id=1,
                Name="RRR",
                Plot ="DADkl",
                YearOfRelease=2024,
                ProducerId=1,
                Poster="www.google.com"
            },
           new MovieDB{
                Id=2,
                Name="RRR2",
                Plot ="DADkl",
                YearOfRelease=2020,
                ProducerId=2,
                Poster="www.google.com"
            }
        };
        private static readonly List<List<int>> _actors = new List<List<int>> { new List<int>{ 1, 2 }, new List<int>{ 1 } };

        private static readonly List<List<int>> _genres = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 1 } };
        public static Moq.Mock<IMovieRepository> MovieRepositoryMoq = new Moq.Mock<IMovieRepository>();
        public static void MockAll()
        {
           
            MovieRepositoryMoq.Setup(foo => foo.MovieById(It.IsAny<int>())).Returns((int x) => ListOfMovies.FirstOrDefault(y=>y.Id==x));

            MovieRepositoryMoq.Setup(foo => foo.GetMovieActors(It.IsAny<int>())).Returns((int x) => _actors[x - 1]);

            MovieRepositoryMoq.Setup(foo => foo.GetMovieGenres(It.IsAny<int>())).Returns((int x) =>_genres[x - 1]);

            MovieRepositoryMoq.Setup(foo => foo.UpdateMovie(It.IsAny<MovieRequest>(),It.IsAny<int>()));
            MovieRepositoryMoq.Setup(foo => foo.DeleteMovie(It.IsAny<int>()));
            MovieRepositoryMoq.Setup(foo => foo.AddMovie(It.IsAny<MovieRequest>()));
            MovieRepositoryMoq.Setup(foo => foo.GetAllMovies(It.IsAny<int>())).Returns((int x)=>ListOfMovies.Where(y=>y.YearOfRelease==x).ToList());
        }
    }
}
