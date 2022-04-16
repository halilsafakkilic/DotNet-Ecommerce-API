using System;
using System.Threading;
using System.Threading.Tasks;
using CartAPI.Actions.FindProduct;
using CartAPI.Actions.SaveToCart;
using CartAPI.DTOs;
using CartAPI.Infrastructure;
using Moq;
using Xunit;

namespace CartAPIUnitTest.AddToCart
{
    public class AddToCartWithValidProductTest : AddToCartWithValidProductTestFixture
    {
        [Fact]
        public async void ValidProductTest()
        {
            Exception ex = null;
            try
            {
                var query = new AddToCartCommand(_findProductQuery.Object, _cartService.Object);

                var request = new AddToCartRequestDto
                {
                    Sku = "test1",
                    AmountInOrder = 5
                };
                await query.Call(request, CancellationToken.None);
            }
            catch (Exception e)
            {
                ex = e;
            }

            Assert.Null(ex);
        }
    }

    public class AddToCartWithValidProductTestFixture
    {
        protected Mock<IFindProductQuery> _findProductQuery = new Mock<IFindProductQuery>();
        protected Mock<ICartService> _cartService = new Mock<ICartService>();

        public AddToCartWithValidProductTestFixture()
        {
            _findProductQuery.Setup(x => x.SendRequestAsync(It.IsAny<AddToCartRequestDto>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.FromResult(new ProductItemDto
                 {
                     Name = "Test #1",
                     StockAmount = 5
                 }));
        }
    }
}