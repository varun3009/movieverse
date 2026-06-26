using AutoMapper;
using IMDBAPI.Exceptions;
using IMDBAPI.Models.RequestModels;
using IMDBAPI.Models.ResponseModels;
using IMDBAPI.Repository.Interfaces;
using IMDBAPI.Services.Interfaces;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using IMDBAPI.Models.DBModels;
using System;


namespace IMDBAPI.Services
{
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public List<GenreResponse> GetGenres()
        {
            return _mapper.Map<List<GenreResponse>>(_genreRepository.GetAllGenres());
        }
        private bool isPurestring(string p0)
        {
            var regex = new Regex(@"[A-Za-z.,]+");
            bool res = regex.IsMatch(p0);
            return res;
        }
        public int AddGenre(GenreRequest genre)
        {

            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new InvalidInputException("name is empty");
            if (!isPurestring(genre.Name))
                throw new InvalidInputException("Name is not valid");
            return _genreRepository.AddGenre(genre);
        }
        public void ValidateGenreId(int id)
        {
            var Genre = _genreRepository.GenreById(id);
            if (Genre == null)
                throw new NotFoundException("Genre with given id doesn't exist");
        }
        public GenreResponse GetGenreById(int id)
        {
            var genre = _genreRepository.GenreById(id);
            if (genre == null)
                throw new NotFoundException("Genre with given id doesn't exist");
            return _mapper.Map<GenreResponse>(genre);
        }
        public void DeleteGenre(int id)
        {
            ValidateGenreId(id);
            try
            {
                _genreRepository.DeleteGenre(id);
            }
            catch (Exception e)
            {
                throw new InternalServerException("Genre can't be deleted");
            }
        }
        public void UpdateGenre(GenreRequest genre, int id)
        {
            ValidateGenreId(id);
            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new InvalidInputException("name is empty");
            if (!isPurestring(genre.Name))
                throw new InvalidInputException("Name is not valid");
            _genreRepository.UpdateGenre(genre, id);
        }
    }
}
