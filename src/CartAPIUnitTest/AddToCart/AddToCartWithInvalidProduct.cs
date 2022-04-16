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
    public class AddToCartWithInvalidProduct : AddToCartWithInvalidProductFixture
    {
        [Fact]
        public async void UndefinedProductTest()
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

            Assert.NotNull(ex);
            Assert.Contains("product is not found", ex.Message);
        }
    }

    public class AddToCartWithInvalidProductFixture
    {
        protected Mock<IFindProductQuery> _findProductQuery = new Mock<IFindProductQuery>();
        protected Mock<ICartService> _cartService = new Mock<ICartService>();

        public AddToCartWithInvalidProductFixture()
        {
            _findProductQuery.Setup(x => x.SendRequestAsync(It.IsAny<AddToCartRequestDto>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.FromResult(default(ProductItemDto)));
        }
    }
}