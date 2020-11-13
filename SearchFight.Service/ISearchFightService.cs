using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Service
{
    public interface ISearchFightService
    {
        Task<IEnumerable<SearchFightResponse>> GetSearchFightResponses(string[] technologies);
    }
}
