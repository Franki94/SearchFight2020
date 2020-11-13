using System;

namespace SearchFight.Exceptions
{
    public class ApiClientExceptions : Exception
    {
        public ApiClientExceptions(string message) : base(message)
        {
        }
    }
}
