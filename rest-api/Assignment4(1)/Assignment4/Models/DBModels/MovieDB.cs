namespace IMDBAPI.Models.DBModels
{
    public class MovieDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plot { get; set; }
        public int YearOfRelease { get; set;}
        public int ProducerId { get; set; }
        public string Poster { get; set; }

        public string Caption { get; set; }

    }
}
