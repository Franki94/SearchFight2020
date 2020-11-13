using Moq;
using SearchFight.Searcher;

namespace SearchFight.UnitTests.Mock
{
    public class SearcherMockBuilder
    {
        private readonly Mock<ISearcher> _mock;
        public SearcherMockBuilder()
        {
            _mock = new Mock<ISearcher>();
        }
        public SearcherMockBuilder AddName(string name) 
        {
            _mock.Setup(x => x.SearcherName).Returns(name);
            return this;
        }

        public SearcherMockBuilder GetSearchResultsCountReturning10() 
        {
            _mock.Setup(x => x.GetSearchResultsCount(It.IsAny<string>())).ReturnsAsync(10);
            return this;
        }

        public ISearcher Build()
        {
            return _mock.Object;
        }
    }
}
