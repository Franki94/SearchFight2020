using System.Collections.Generic;

namespace SearchFight.Service
{
    public interface ISearcherCompetition
    {
        IEnumerable<SearchFightResponse> GetSearchFightWinnerPerSearcher(IEnumerable<SearchFightResponse> searchFightResponses);
        string GetSearchFightTotalWinnerTech(IEnumerable<SearchFightResponse> searchFightResponses);
    }
}
