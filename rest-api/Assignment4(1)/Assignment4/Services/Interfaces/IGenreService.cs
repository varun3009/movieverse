using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;

namespace IMDBAPI.Services.Interfaces
{
    public interface IGenreService
    {
        List<GenreResponse> GetGenres();
        public int AddGenre(GenreRequest Genre);
        public GenreResponse GetGenreById(int id);
        public void UpdateGenre(GenreRequest Genre, int id);
        public void DeleteGenre(int id);
    }
}
