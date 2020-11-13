using FluentAssertions;
using NUnit.Framework;
using SearchFight.Exceptions;
using SearchFight.Searcher;
using SearchFight.Searcher.Google;
using SearchFight.UnitTests.Mock;
using System;
using System.Threading.Tasks;

namespace SearchFight.UnitTests.SearcherTests
{
    [TestFixture]
    public class GoogleSearcherTests
    {
        private HttpClientMockBuilder _httpClientMock;
        private ISearcher _searcher;
        [SetUp]
        public void Init()
        {
            _httpClientMock = new HttpClientMockBuilder();

            _searcher = new GoogleSearcher(_httpClientMock.Build(), new GoogleSearcherSecret());
        }

        [Test]
        public void GetSearchResultsCount_FailedCall_ThrowsApiClientExceptions()
        {
            //Arrange
            _httpClientMock.WithFailedSendAsync();

            //Action
            Func<Task> action = async () => await _searcher.GetSearchResultsCount("c#");
            //Assert
            action.Should().ThrowAsync<ApiClientExceptions>().WithMessage("Failed in Google Search c#");
        }
        [Test]
        public async Task GetSearchResultsCount_SuccessCall_ReturnsValue()
        {
            //Arrange
            _httpClientMock.WithSuccessfullSendAsyncGoogleSearchResponse();

            //Action
            var response = await _searcher.GetSearchResultsCount("c#");

            //Assert
            response.Should().Be(100);
        }
    }
}
