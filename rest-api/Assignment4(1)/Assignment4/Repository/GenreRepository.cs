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
    public class GenreRepository:BaseRepository<GenreDB>,IGenreRepository
    {
        private readonly IMapper _mapper;
        public GenreRepository(IMapper mapper, IOptions<ConnectionString> options) : base(options.Value.IMDBDB)
        {
            _mapper = mapper;
            
        }
        public int AddGenre(GenreRequest genre)
        {
            var query = @"INSERT INTO Foundation.Genres
            (NAME)
            OUTPUT INSERTED.Id
            VALUES      (@Name) ";
            return QueryPerson(query, genre);
            
        }
        
        public void UpdateGenre(GenreRequest genre, int id)
        {
            GenreDB genredb =_mapper.Map<GenreDB>(genre);
            genredb.Id = id;
            var query = @"UPDATE Foundation.Genres
            SET    NAME = @Name
            WHERE  id = @Id ";
            ExecuteQuery(query, genredb);
            
        }
        public void DeleteGenre(int id)
        {

            
            var query = @"DELETE FROM Foundation.Genres
            WHERE  id = @id ";
            ExecuteQuery(query, new { id = id });
            
        }
        public GenreDB GenreById(int id)
        {

            string query = @"SELECT NAME,
            id
            FROM   Foundation.Genres
            WHERE  id = @Id ";
            var item = Get(query, new { Id = id });
            return item;
        }
        public List<GenreDB> GetAllGenres()
        {
            string query = @"SELECT NAME,
            id
            FROM   Foundation.Genres ";
            var items = GetAll(query);
            return items.ToList();
        }

    }
}