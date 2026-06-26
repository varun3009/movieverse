using System;
namespace IMDBAPI.Models.RequestModels
{
    public class PersonRequest
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Sex { get; set; }
        public DateTime Dob { get; set; }
       
    }
}
