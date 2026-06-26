using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using Moq;
using IMDBAPI.Models.DBModels;
using AutoMapper;
using IMDBAPI.Helpers;

namespace IMDB.Tests.Mock
{
    public class GenreRepositoryMock
    {
        public static Mock<IGenreRepository> GenreRepositoryMoq;
        private static readonly IEnumerable<GenreDB> ListOfGenres = new List<GenreDB>
        {
            new GenreDB
            {
                Id = 1,
                Name = "A1",
            },
            new GenreDB
            {
                Id = 2,
                Name = "A2",
            }
        };
        public GenreRepositoryMock()
        {
        }
        public static void MockAll()
        {
            GenreRepositoryMoq = new Mock<IGenreRepository>();
            GenreRepositoryMoq.Setup(foo => foo.AddGenre(It.IsAny<GenreRequest>()));
            GenreRepositoryMoq.Setup(foo => foo.UpdateGenre(It.IsAny<GenreRequest>(), It.IsAny<int>()));
            GenreRepositoryMoq.Setup(foo => foo.DeleteGenre(It.IsAny<int>()));
            GenreRepositoryMoq.Setup(foo => foo.GenreById(It.IsAny<int>())).Callback<int>(x => Console.WriteLine(x)).Returns((int x) => ListOfGenres.FirstOrDefault(z => z.Id == x));
            GenreRepositoryMoq.Setup(foo => foo.GetAllGenres()).Returns(ListOfGenres.ToList());
        }

    }
}
