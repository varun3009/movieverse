using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using System.Linq;
using System.Collections.Generic;
using IMDBAPI.Models.DBModels;
namespace IMDBAPI.Repository.Interfaces
{
    public interface IGenreRepository
    {

        int AddGenre(GenreRequest genre);
        
        void UpdateGenre(GenreRequest genre, int id);
        void DeleteGenre(int id);
        GenreDB GenreById(int id);
        List<GenreDB> GetAllGenres();
    }
}
