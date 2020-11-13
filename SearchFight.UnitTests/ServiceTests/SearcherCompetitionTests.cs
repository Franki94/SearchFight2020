using FluentAssertions;
using NUnit.Framework;
using SearchFight.Service;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight.UnitTests.ServiceTests
{
    [TestFixture]
    public class SearcherCompetitionTests
    {
        private SearcherCompetition _service;

        [SetUp]
        public void Config()
        {
            _service = new SearcherCompetition();
        }

        #region GetSearchFightWinnerPerSearcher
        [Test]
        public void GetSearchFightWinnerPerSearcher_OneSearcherTwoTechnologiesJsWinner_ReturnsJsSearcher1Winner()
        {
            //Arrange
            var param = new List<SearchFightResponse>
            {
                new SearchFightResponse{ SearcherName = "searcher1", Technology = "c#", TotalResultsCount = 10 },
                new SearchFightResponse{ SearcherName = "searcher1", Technology = "js", TotalResultsCount = 20 }
            };


            //Action
            var result = _service.GetSearchFightWinnerPerSearcher(param);

            //Assert
            result.Should().AllBeOfType<SearchFightResponse>().And.HaveCount(1);
            result.First().Should().BeEquivalentTo(new SearchFightResponse { SearcherName = "searcher1", Technology = "js", TotalResultsCount = 20 });
        }

        [Test]
        public void GetSearchFightWinnerPerSearcher_TwoSearchersTwoTechnologies_ReturnsJsWinnerInBothSearchers()
        {
            //Arrange
            var param = new List<SearchFightResponse>
            {
                new SearchFightResponse{ SearcherName = "searcher1", Technology = "c#", TotalResultsCount = 10 },
                new SearchFightResponse{ SearcherName = "searcher1", Technology = "js", TotalResultsCount = 20 },
                new SearchFightResponse{ SearcherName = "searcher2", Technology = "c#", TotalResultsCount = 30 },
                new SearchFightResponse{ SearcherName = "searcher2", Technology = "js", TotalResultsCount = 40 }
            };

            var expectedResult = new List<SearchFightResponse>
            {
                new SearchFightResponse { SearcherName = "searcher1", Technology = "js", TotalResultsCount = 20 },
                new SearchFightResponse { SearcherName = "searcher2", Technology = "js", TotalResultsCount = 40 }
            };

            //Action
            var result = _service.GetSearchFightWinnerPerSearcher(param);

            //Assert
            result.Should().AllBeOfType<SearchFightResponse>().And.HaveCount(2);
            result.Should().BeEquivalentTo(expectedResult);
        }
        #endregion

        #region GetSearchFightTotalWinnerTech
        [Test]
        public void GetSearchFightTotalWinnerTech_TwoSearchersJsWinnerInBoth_ReturnsJsWinner()
        {
            //Arrange
            var param = new List<SearchFightResponse>
            {
                new SearchFightResponse{ SearcherName = "searcher1", Technology = "c#", TotalResultsCount = 10 },
                new SearchFightResponse{ SearcherName = "searcher1", Technology = "js", TotalResultsCount = 20 },
                new SearchFightResponse{ SearcherName = "searcher2", Technology = "c#", TotalResultsCount = 30 },
                new SearchFightResponse{ SearcherName = "searcher2", Technology = "js", TotalResultsCount = 40 }
            };


            //Action
            var result = _service.GetSearchFightTotalWinnerTech(param);

            //Assert
            result.Should().Be("js");
        }
        #endregion
    }
}
