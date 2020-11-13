using SearchFight.Service;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight.Orchestrators
{
    public class SearchFightOrchestrator : ISearchFightOrchestrator
    {
        private readonly ISearchFightService _searchFightService;
        private readonly ISearcherCompetition _searcherCompetition;
        public SearchFightOrchestrator(ISearchFightService searchFightService, ISearcherCompetition searcherCompetition)
        {
            _searcherCompetition = searcherCompetition;
            _searchFightService = searchFightService;
        }

        public SearchFightViewTotalResponse GetSearchFightViewTotalResponse(string[] technologies)
        {
            var searchersResponse = _searchFightService.GetSearchFightResponses(technologies).GetAwaiter().GetResult();

            var searchersResponseBySearcher = _searcherCompetition.GetSearchFightWinnerPerSearcher(searchersResponse);
            var searchersResponseTotalWinner = _searcherCompetition.GetSearchFightTotalWinnerTech(searchersResponse);

            return new SearchFightViewTotalResponse
            {
                SearchFightResults = MapToSearchFightView(searchersResponse),
                SearchFightSearcherWinnersResults = MapToSearchFightView(searchersResponseBySearcher),
                SearchFightTechnologyWinner = searchersResponseTotalWinner
            };
        }

        private IEnumerable<SearchFightViewResponse> MapToSearchFightView(IEnumerable<SearchFightResponse> searchFightResponses)
        {
            return searchFightResponses.Select(x => new SearchFightViewResponse { SearcherName = x.SearcherName, Technology = x.Technology, TotalResultsCount = x.TotalResultsCount });
        }
    }
}
