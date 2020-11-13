using SearchFight.Searcher;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.Service
{
    public class SearchFightService : ISearchFightService
    {
        private readonly IEnumerable<ISearcher> _searchers;
        public SearchFightService(IEnumerable<ISearcher> searchers)
        {
            _searchers = searchers;
        }

        public async Task<IEnumerable<SearchFightResponse>> GetSearchFightResponses(string[] technologies)
        {
            var searchResponses = new List<SearchFightResponse>();
            foreach (var tech in technologies)
            {
                foreach (var searcher in _searchers)
                {
                    searchResponses.Add(new SearchFightResponse
                    {
                        SearcherName = searcher.SearcherName,
                        Technology = tech,
                        TotalResultsCount = await searcher.GetSearchResultsCount(tech)
                    });
                }
            }

            return searchResponses;
        }
    }
}
