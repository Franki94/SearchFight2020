﻿using SearchFight.Exceptions;
using SearchFight.Searcher.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SearchFight.Searcher.Google
{
    public class GoogleSearcher : ISearcher
    {
        private readonly HttpClient _httpClient;
        private readonly GoogleSearcherSecret _secret;
        public GoogleSearcher(HttpClient httpClient, GoogleSearcherSecret secret)
        {
            _httpClient = httpClient;
            _secret = secret;
        }

        public string SearcherName => "Google";

        public async Task<long> GetSearchResultsCount(string value)
        {
            var queryStringParams = _secret.Params.ToDictionary(x => x.Name, y => y.Value);          
            queryStringParams.Add("q", value);

            var request = new HttpRequestMessage(HttpMethod.Get, $"customsearch/v1{queryStringParams.ToQueryString()}");
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new ApiClientExceptions($"Failed in Google Search {value}");

            var content = await response.Content.ReadAsStringAsync();
            var objectResponse = content.ToObjectResponse<GoogleSearchResponse>();
            return Convert.ToInt64(objectResponse.SearchInformation.TotalResults);
        }
    }
}
