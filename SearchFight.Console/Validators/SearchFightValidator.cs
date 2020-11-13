using SearchFight.Exceptions;
using System.Linq;

namespace SearchFight.Validators
{
    public class SearchFightValidator
    {
        public void Validate(string[] technologies)
        {            
            if (technologies == null || technologies.Count() < 2)
                throw new ValidatorException("Technologies should have at least 2 values to compare");

            if (technologies.Distinct().Count() != technologies.Count())
                throw new ValidatorException("Technologies should not have duplicate values to compare");
        }
    }
}
