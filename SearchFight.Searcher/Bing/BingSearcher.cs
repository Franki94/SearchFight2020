using SearchFight.Exceptions;
using SearchFight.Searcher.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Searcher.Bing
{
    public class BingSearcher : ISearcher
    {
        private readonly HttpClient _httpClient;
        public BingSearcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string SearcherName => "Bing";

        public async Task<long> GetSearchResultsCount(string value)
        {
            var queryString = new Dictionary<string, string> { ["q"] = value }.ToQueryString();

            var request = new HttpRequestMessage(HttpMethod.Get, $"v7.0/search{queryString}");
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new ApiClientExceptions($"Failed in Bin Seach {value}");

            var content = await response.Content.ReadAsStringAsync();
            var objectResponse = content.ToObjectResponse<BingSearchResponse>();
            return objectResponse.WebPages.TotalEstimatedMatches;
        }
    }
}
