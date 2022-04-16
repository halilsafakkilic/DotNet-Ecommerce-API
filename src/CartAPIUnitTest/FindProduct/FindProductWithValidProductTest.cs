using CartAPI.Actions.FindProduct;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CartAPI.Infrastructure;
using Xunit;

namespace CartAPIUnitTest.FindProduct
{
    public class FindProductWithValidProductTest : FindProductWithValidProductTestFixture
    {
        [Fact]
        public async void ValidProductTest()
        {
            Exception ex = null;
            try
            {
                var query = new FindProductQuery(_httpClientFactory.Object, new ProjectSettings()
                {
                    ApiGatewayUrl = "http://localhost"
                });

                var request = new CartAPI.DTOs.AddToCartRequestDto
                {
                    Sku = "test1",
                    AmountInOrder = 1
                };
                await query.SendRequestAsync(request, CancellationToken.None);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.Null(ex);
        }
    }

    public class FindProductWithValidProductTestFixture
    {
        protected Mock<IHttpClientFactory> _httpClientFactory;

        public FindProductWithValidProductTestFixture()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'Name':'Test','SKU':'test1','StockAmount':3}"),
                });


            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactory.Setup(_ => _.CreateClient("productService")).Returns(client);
        }
    }
}