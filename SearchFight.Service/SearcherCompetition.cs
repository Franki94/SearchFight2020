using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Service
{
    public class SearcherCompetition : ISearcherCompetition
    {
        public IEnumerable<SearchFightResponse> GetSearchFightWinnerPerSearcher(IEnumerable<SearchFightResponse> searchFightResponses)
        {
            var groupBySearcher = searchFightResponses.GroupBy(x => x.SearcherName);
            return groupBySearcher.Select(x => x.OrderByDescending(x => x.TotalResultsCount)).Select(x => x.First());
        }

        public string GetSearchFightTotalWinnerTech(IEnumerable<SearchFightResponse> searchFightResponses)
        {
            var winnerTechGroup = searchFightResponses.GroupBy(x => x.Technology);
            var winner = winnerTechGroup.Select(x => new { Technology = x.Key, TotalResultsCount = x.Sum(w => w.TotalResultsCount) }).OrderByDescending(x => x.TotalResultsCount);
            return winner.First().Technology;
        }
    }
}
