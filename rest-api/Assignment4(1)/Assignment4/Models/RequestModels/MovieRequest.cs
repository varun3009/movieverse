namespace IMDBAPI.Models.RequestModels
{
    public class MovieRequest
    { 
        public string Name { get; set; }
        public string Plot { get; set; }
        public int YearOfRelease { get; set;}
        public int ProducerId { get; set; }
        public string Actors {  get; set; }
        public string Genres { get; set; }
        public string Poster { get; set; }

    }
}
