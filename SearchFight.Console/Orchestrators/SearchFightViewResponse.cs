using System.Collections.Generic;

namespace SearchFight.Orchestrators
{
    public class SearchFightViewTotalResponse
    {
        public SearchFightViewTotalResponse()
        {
            SearchFightResults = new List<SearchFightViewResponse>();
            SearchFightSearcherWinnersResults = new List<SearchFightViewResponse>();
        }
        public IEnumerable<SearchFightViewResponse> SearchFightResults { get; set; }
        public IEnumerable<SearchFightViewResponse> SearchFightSearcherWinnersResults { get; set; }
        public string SearchFightTechnologyWinner { get; set; }

    }
    public class SearchFightViewResponse
    {
        public string Technology { get; set; }
        public string SearcherName { get; set; }
        public long TotalResultsCount { get; set; }
    }


}
