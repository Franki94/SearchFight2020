using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SearchFight.Searcher.Bing;
using SearchFight.Searcher.Google;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SearchFight.UnitTests.Mock
{
    public class HttpClientMockBuilder
    {
        private readonly Mock<HttpMessageHandler> _messageHandler;
        public HttpClientMockBuilder()
        {
            _messageHandler = new Mock<HttpMessageHandler>();
        }

        public HttpClientMockBuilder WithSuccessfullSendAsyncBingSearchResponse()
        {
            _messageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new BingSearchResponse { WebPages = new WebPages { TotalEstimatedMatches = 100 } })),
                });
            return this;
        }
        public HttpClientMockBuilder WithSuccessfullSendAsyncGoogleSearchResponse()
        {
            _messageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(new GoogleSearchResponse { SearchInformation = new SearchInformation { TotalResults = "100" } })),
                });
            return this;
        }
        public HttpClientMockBuilder WithFailedSendAsync()
        {
            _messageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("error"),
                });
            return this;
        }
        public HttpClient Build()
        {
            var httpClient = new HttpClient(_messageHandler.Object) { BaseAddress = new Uri("http://test.com/"), };
            //var factory = new Mock<IHttpClientFactory>();
            //factory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient);
            //return factory.Object;
            return httpClient;
        }
    }
}
