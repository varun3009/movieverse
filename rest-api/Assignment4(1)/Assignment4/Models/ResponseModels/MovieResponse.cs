
using System.Linq;
using System.Collections.Generic;
namespace IMDBAPI.Models.ResponseModels
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public int YearOfRelease { get; set;}
        public ProducerResponse Producer { get; set; }
        public List<ActorResponse> Actors { get; set; }
        public List<GenreResponse> Genres { get; set; }
        public string Poster { get; set; }

        public string Caption { get; set; }
    }
}
