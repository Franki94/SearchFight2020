using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using NUnit.Framework;
using SearchFight.Exceptions;
using SearchFight.Searcher;
using SearchFight.Searcher.Bing;
using SearchFight.UnitTests.Mock;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SearchFight.UnitTests.SearcherTests
{
    [TestFixture]
    public class BingSearcherTests
    {
        private HttpClientMockBuilder _httpClientMock;
        private ISearcher _searcher;
        [SetUp]
        public void Init()
        {
            _httpClientMock = new HttpClientMockBuilder();

            _searcher = new BingSearcher(_httpClientMock.Build());
        }

        [Test]
        public void GetSearchResultsCount_FailedCall_ThrowsApiClientExceptions() 
        {
            //Arrange
            _httpClientMock.WithFailedSendAsync();

            //Action
            Func<Task> action = async () => await _searcher.GetSearchResultsCount("c#");
            //Assert
            action.Should().ThrowAsync<ApiClientExceptions>().WithMessage("Failed in Bin Seach c#");
        }
        [Test]
        public async Task GetSearchResultsCount_SuccessCall_ReturnsValue()
        {
            //Arrange
            _httpClientMock.WithSuccessfullSendAsyncBingSearchResponse();

            //Action
            var response =  await _searcher.GetSearchResultsCount("c#");

            //Assert
            response.Should().Be(100);
        }

    }  
}
