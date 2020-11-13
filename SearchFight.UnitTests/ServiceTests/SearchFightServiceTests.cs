using FluentAssertions;
using NUnit.Framework;
using SearchFight.Searcher;
using SearchFight.Service;
using SearchFight.UnitTests.Mock;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SearchFight.UnitTests.ServiceTests
{
    [TestFixture]
    public class SearchFightServiceTests
    {
        private SearchFightService _service;
        private SearcherMockBuilder _searcher1;
        private SearcherMockBuilder _searcher2;

        [SetUp]
        public void Config()
        {
            _searcher1 = new SearcherMockBuilder();
            _searcher2 = new SearcherMockBuilder();
            _searcher1.AddName("searcher1");
            _searcher2.AddName("searcher2");
            _service = new SearchFightService(new List<ISearcher>
            {
                _searcher1.Build(),
                _searcher2.Build()
            });
        }

        [Test]
        public async Task GetSearchFightResponses_LookingForCSharpInTwoSearchers_ReturnsTwoItems()
        {
            //Arrange
            _searcher1.GetSearchResultsCountReturning10();
            _searcher2.GetSearchResultsCountReturning10();
            var expectedResult = new List<SearchFightResponse> 
            {
                new SearchFightResponse { Technology = "c#", SearcherName = "searcher1", TotalResultsCount = 10 },
                new SearchFightResponse { Technology = "c#", SearcherName = "searcher2", TotalResultsCount = 10 }
            };
            var param = new string[] { "c#" };

            //Action
            var result = await _service.GetSearchFightResponses(param);

            //Assert

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetSearchFightResponses_EmptyArray_ReturnsEmptyList()
        {
            //Arrange
            _searcher1.GetSearchResultsCountReturning10();
            _searcher2.GetSearchResultsCountReturning10();
                       
            //Action
            var result = await _service.GetSearchFightResponses(new string[0]);

            //Assert

            result.Should().BeEquivalentTo(new List<SearchFightResponse>());
        }
    }
}
