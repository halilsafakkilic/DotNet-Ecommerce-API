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
    public class FindProductWithUndefinedProduct : FindProductWithInvalidProductFixture
    {
        [Fact]
        public async void UndefinedProductTest()
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

            Assert.NotNull(ex);
            Assert.Contains("NotFound", ex.Message);
        }
    }

    public class FindProductWithInvalidProductFixture
    {
        protected Mock<IHttpClientFactory> _httpClientFactory;

        public FindProductWithInvalidProductFixture()
        {
            _httpClientFactory = new Mock<IHttpClientFactory>();

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(""),
                });


            var client = new HttpClient(mockHttpMessageHandler.Object);
            _httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        }
    }
}