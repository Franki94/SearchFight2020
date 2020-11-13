using System;

namespace SearchFight.Exceptions
{
    public class ValidatorException : Exception
    {
        public ValidatorException(string message) : base(message)
        {
        }
    }
}
