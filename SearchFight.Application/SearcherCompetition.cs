using SearchFight.Service;

namespace SearchFight.Application
{
    public class SearcherCompetition
    {
        private readonly ISearchFightService _searchFightService;
        public SearcherCompetition(ISearchFightService searchFightService)
        {
            _searchFightService = searchFightService;
        }


    }
}
