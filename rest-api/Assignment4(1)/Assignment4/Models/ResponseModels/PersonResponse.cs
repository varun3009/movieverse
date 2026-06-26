using System;
namespace IMDBAPI.Models.ResponseModels
{
    public class PersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Sex { get; set; }
        public DateTime Dob { get; set; }
       
    }
}
