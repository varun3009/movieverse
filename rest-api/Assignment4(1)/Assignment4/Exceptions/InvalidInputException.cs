using System.Net;
using System.Net.Http;

namespace IMDBAPI.Exceptions
{
    public class InvalidInputException:HttpRequestException
    {
        public InvalidInputException(string message):base(message,null,HttpStatusCode.BadRequest){ 
        }
    }
}
