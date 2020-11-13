using System.Threading.Tasks;

namespace SearchFight.Searcher
{
    public interface ISearcher
    {
        string SearcherName { get; }
        Task<long> GetSearchResultsCount(string value);
    }
}
