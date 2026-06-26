using System;

namespace IMDBAPI.Exceptions
{
    public class InternalServerException:Exception
    {
        public InternalServerException(string msg):base(msg)
        {

        }
    }
}
