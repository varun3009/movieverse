using System;

namespace IMDBAPI.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(string msg):base(msg) { }
    }
}
