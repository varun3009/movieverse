namespace IMDBAPI.Models.DBModels
{
    public class ReviewDB
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int MovieId { get; set; }
    }
}
